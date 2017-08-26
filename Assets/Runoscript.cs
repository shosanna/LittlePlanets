using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runoscript : MonoBehaviour {
    public Sprite AktivniRuna;
    public Sprite DefaultRuna;
    public AudioClip RunoZvuk;
    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        _spriteRenderer.sprite = AktivniRuna;
        AudioSource.PlayClipAtPoint(RunoZvuk, transform.position);
    }

    public void SmazRunu()
    {
        _spriteRenderer.sprite = DefaultRuna;
    }
}
