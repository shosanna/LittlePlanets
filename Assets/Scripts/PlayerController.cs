using Coords;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private SpriteRenderer _renderer;
    private Animator _anim;

    public Transform cameraTransform;
    public float Radius;

    public float yVelocity = 0;

    public PolarCoord PolarCoord;

    private void Start()
    {
        PolarCoord = new PolarCoord(1, Radius);
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    void Update () {
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
        var movement = Input.GetAxisRaw("Horizontal");

        if (movement == 1)
        {
            //_renderer.flipX = false;
            _anim.SetBool("isMoving", true);
        }
        else if (movement == -1)
        {
            //_renderer.flipX = true;
            _anim.SetBool("isMoving", true);
        }
        else
        {
            _anim.SetBool("isMoving", false);
        }

            yVelocity -= 3 * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = 1;
            _anim.SetTrigger("Jump");
        }

        PolarCoord.R += yVelocity * Time.deltaTime;
        if (PolarCoord.R < Radius)
        {
            yVelocity = 0;
            PolarCoord.R = Radius;
        }
        PolarCoord.Phi += -movement * (Mathf.PI / 5) * Time.deltaTime;

        transform.localPosition = PolarCoord.ToCartesian().ToVector3();
        transform.rotation = Quaternion.EulerRotation(0, 0, PolarCoord.Phi - Mathf.PI / 2);

        //_rigidbody2D.velocity = new Vector2(movement * walkSpeed, _rigidbody2D.velocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.parent.transform.position, Radius);
    }
}
