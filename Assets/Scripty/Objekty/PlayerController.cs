using Coords;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
    private Animator _anim;

    public Transform cameraTransform;
    public Transform branoTransform;
    public float Radius;
    public float yVelocity = 0;
    public float speed = 5;
    public PolarCoord PolarCoord;
    private GameObject _cilAkce;

    private bool _isGrounded = true;

    private void Start() {
        // postava zacina na brane krom poprve (pro tutorial)
        if (branoTransform && !GameState.Instance.RunTutorial) {
            var cartesianBrano = new CartesianCoord(branoTransform.transform.localPosition.x,
                branoTransform.transform.localPosition.y);
            PolarCoord = new PolarCoord(cartesianBrano.ToPolar().R, cartesianBrano.ToPolar().Phi);
        } else {
            PolarCoord = new PolarCoord(1, Radius);
        }

        _anim = GetComponent<Animator>();
        origCamera = Camera.main.transform;
    }

    void Update() {
        // pro dvoj hopik
        _isGrounded = PolarCoord.R > Radius;

        // sledovani hrace
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);

        // pohyb
        var movement = Input.GetAxisRaw("Horizontal");
        if (movement == 1) {
            //_renderer.flipX = false;
            _anim.SetBool("isMoving", true);
        } else if (movement == -1) {
            //_renderer.flipX = true;
            _anim.SetBool("isMoving", true);
        } else {
            _anim.SetBool("isMoving", false);
        }

        yVelocity -= 5 * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space) && !_isGrounded) {
            yVelocity = 1;
            _anim.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.X) && _cilAkce != null && _cilAkce.GetComponent<Stromoscript>() != null) {
            _anim.SetTrigger("Chop"); // na konci animace se vola Seknuto()
        }

        if (Input.GetKeyDown(KeyCode.X) && _cilAkce != null && _cilAkce.GetComponent<Keroscript>() != null) {
            _anim.SetTrigger("Trhej"); // Na konci animace se vola Trhani()
        }

        if (Input.GetKeyDown(KeyCode.X) && _cilAkce != null && _cilAkce.GetComponent<CropController>() != null)
        {
            // TODO: Animace?? 
            Zasad();
        }

        // beh
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = 3;
        } else {
            speed = 5;
        }

        PolarCoord.R += yVelocity * Time.deltaTime;
        if (PolarCoord.R < Radius) {
            yVelocity = 0;
            PolarCoord.R = Radius;
        }

        // 0.68 je radius originalni planety, toto upravi rychlost pro jinak velke planety
        PolarCoord.Phi += -movement * (Mathf.PI / speed) * Time.deltaTime * 0.68f / Radius;
        // oprava polarnich souradnic pro chuzi po jizni polokouli
        if (PolarCoord.Phi < 0) {
            PolarCoord.Phi = PolarCoord.Phi + (2 * Mathf.PI);
        }
        PolarCoord.Phi = PolarCoord.Phi % (2 * Mathf.PI);

        transform.localPosition = PolarCoord.ToCartesian().ToVector3();

        // nataceni spritu
        transform.rotation = Quaternion.EulerRotation(0, 0, PolarCoord.Phi - Mathf.PI / 2);

        // shake obrazovky pri sekani stromu
        if (shake) {
            float shakeOffset = Random.RandomRange(0, 0.015f);
            Camera.main.transform.position = origCamera.position + new Vector3(shakeOffset, shakeOffset, shakeOffset);
        } else {
            Camera.main.transform.position = origCamera.position;
        }
    }


    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.parent.transform.position, Radius);
    }

    // Je volano na konci animace
    public void Seknuto() {
        if (_cilAkce != null && _cilAkce.GetComponent<Stromoscript>() != null) {
            origCamera = Camera.main.transform;
            StartCoroutine(Neshake());
            shake = true;
            _cilAkce.GetComponent<Stromoscript>().Seknuto();
        }
    }

    // Je volano na konci animace
    public void Trhani() {
        if (_cilAkce != null && _cilAkce.GetComponent<Keroscript>() != null) {
            _cilAkce.GetComponent<Keroscript>().Trhani();
        }
    }

    public void Zasad()
    {
        if (_cilAkce != null && _cilAkce.GetComponent<CropController>() != null)
        {
            _cilAkce.GetComponent<CropController>().Zasad();
        }
    }

    IEnumerator Neshake() {
        yield return new WaitForSeconds(0.07f);
        shake = false;
    }

    Transform origCamera;
    bool shake = false;

    public void NastavCil(GameObject cil) {
        _cilAkce = cil;
    }

    public void ZrusCil() {
        _cilAkce = null;
    }
}