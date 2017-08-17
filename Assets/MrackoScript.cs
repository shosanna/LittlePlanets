using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrackoScript : MonoBehaviour {
    public float minSpeed;
    public float maxSpeed;
    public float minY;
    public float maxY;
    public float buffer;
    public float startX;

    float speed;
    float camWidth;

    // Use this for initialization
    void Start () {
        camWidth = Camera.main.orthographicSize * Camera.main.aspect;
        speed = Random.Range(minSpeed, maxSpeed);
        transform.position = new Vector3(startX, Random.Range(minY, maxY), transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if (transform.position.x - buffer > camWidth)
        {
            transform.position = new Vector3(startX, Random.Range(minY, maxY), transform.position.z);
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
    }
}
