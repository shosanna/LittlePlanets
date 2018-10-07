using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HejbatkoDekoraceScript : MonoBehaviour {
    public float Speed = 0.001f;
    public Vector3 Direction = Vector3.right;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Direction * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Direction = -Direction;
        transform.localPosition = new Vector3(transform.localPosition.x, Random.Range(-1f, 1f), transform.localPosition.z);
    }
}
