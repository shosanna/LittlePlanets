using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
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

    void OnMouseDown()
    {
        bool vysledek = _branoscript.ZapisRunu(Hodnota);

        if (vysledek)
        {
            RunaZmacknuta();
        }
    }

    public void RunaZmacknuta()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = AktivniRuna;
            AudioSource.PlayClipAtPoint(RunoZvuk, Camera.main.transform.position, 1f);
        }
    }

    public void SmazRunu()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = DefaultRuna;
        }
    }
}
