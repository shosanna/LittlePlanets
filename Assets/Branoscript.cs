using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branoscript : MonoBehaviour {
    public GameObject Kniha;
    public Sprite DefaultBrana;
    public Sprite AktivniBrana;

    private bool _aktivniBrana = false;
    private SpriteRenderer _spriteRenderer;

	void Start () {
        Kniha.SetActive(false);
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0) && _aktivniBrana) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            
            // Pokud se kliknulo na tento gameObject = na branu
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                // Toglovani otevirani a zavirani knihy - pouze sprite
                if (Kniha.activeInHierarchy)
                {
                    Kniha.SetActive(false);
                }
                else
                {
                    Kniha.SetActive(true);
                }
            }
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _spriteRenderer.sprite = AktivniBrana;
        _aktivniBrana = true;
    }

    // Kdyz se leavne kolize (s hracem)
    private void OnTriggerExit2D(Collider2D collision)
    {
        _spriteRenderer.sprite = DefaultBrana;
        _aktivniBrana = false;
        Kniha.SetActive(false);
    }
}
