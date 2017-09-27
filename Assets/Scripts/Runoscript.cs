using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runoscript : MonoBehaviour {
    public Sprite AktivniRuna;
    public Sprite DefaultRuna;
    public AudioClip RunoZvuk;
    public string Hodnota;

    private SpriteRenderer _spriteRenderer;
    private Branoscript _branoscript;

	// Use this for initialization
	void Start () {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _branoscript = GetComponentInParent<Branoscript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        bool vysledek = _branoscript.ZapisRunu(Hodnota);

        if (vysledek)
        {
            _spriteRenderer.sprite = AktivniRuna;
            AudioSource.PlayClipAtPoint(RunoZvuk, Camera.main.transform.position);
        }
    }

    public void SmazRunu()
    {
        _spriteRenderer.sprite = DefaultRuna;
    }
}
