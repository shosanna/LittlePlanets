using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody2D;
    public float walkSpeed = 3;
    public Transform cameraTransform;

    // Use this for initialization
    void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        cameraTransform.position = new Vector3(transform.position.x + 3, transform.position.y, cameraTransform.position.z);
        var movement = Input.GetAxisRaw("Horizontal");

        if (movement == 1)
        {
            _renderer.flipX = false;
            //_anim.SetBool("isMoving", true);
        }
        else if (movement == -1)
        {
            _renderer.flipX = true;
            //_anim.SetBool("isMoving", true);
        }
        else
        {
            //_anim.SetBool("isMoving", false);
        }

        _rigidbody2D.velocity = new Vector2(movement * walkSpeed, _rigidbody2D.velocity.y);
    }
}
