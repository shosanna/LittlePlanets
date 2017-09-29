using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branoscript : MonoBehaviour {
    public GameObject Kniha;
    public Sprite DefaultBrana;
    public Sprite AktivniBrana;
    public string Adresa;
    public Branosystem BranoSystem;
    public string Scena;

    private string _aktualniAdresa;
    private bool _aktivniBrana = false;
    private SpriteRenderer _spriteRenderer;
    private Runoscript[] _runy;

    ///////////////
    /// PUBLIC ///
    ///////////////
    public bool ZapisRunu(string runa)
    {
        // nedovol zapsat stejnou runu dvakrat
        if (_aktualniAdresa.Contains(runa)) return false;

        if (_aktualniAdresa.Length < 3)
        {
            _aktualniAdresa += runa;

            return true;
        }

        if (_aktualniAdresa.Length == 3)
        {
            if (_aktualniAdresa == Adresa)
            {
                ResetniKnihu();
                return false;
            }

            BranoSystem.VytocBranu(_aktualniAdresa, this);
        }

        return false;
    }

    public void ResetniKnihu()
    {
        _aktualniAdresa = "";
        for (int i = 0; i < _runy.Length; i++)
        {
            _runy[i].SmazRunu();
        }
    }

    ///////////////
    /// PRIVATE ///
    ///////////////
    private void Start () {
        Kniha.SetActive(false);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _runy = GetComponentsInChildren<Runoscript>(true);
        _aktualniAdresa = "";
        Debug.Log(Scena);
	}
	
	private void Update () {
        if (Input.GetMouseButtonDown(0) && _aktivniBrana) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            
            // Pokud se kliknulo na tento gameObject = na branu
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                // Toglovani otevirani a zavirani knihy - pouze sprite
                if (Kniha.activeInHierarchy)
                {
                    ZavriKnihu();
                }
                else
                {
                    Kniha.SetActive(true);
                }
            }
        }

        RunaZmacknuta(KeyCode.R, "1");
        RunaZmacknuta(KeyCode.F, "2");
        RunaZmacknuta(KeyCode.T, "3");
        RunaZmacknuta(KeyCode.G, "4");
    }

    private void RunaZmacknuta(KeyCode klavesa, string runokod)
    {
        // Pokud jsme zmacknuly zapisovani runy a brana i kniha je aktivni
        if (Input.GetKeyDown(klavesa) && _aktivniBrana && Kniha.activeInHierarchy)
        {
            bool vysledek = ZapisRunu(runokod);
            if (vysledek)
            {
                for (int i = 0; i < _runy.Length; i++)
                {
                    if (_runy[i].Hodnota == runokod)
                    {
                        _runy[i].RunaZmacknuta();
                    }
                }
            }
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fungus.Flowchart.BroadcastFungusMessage("SpustBranoNapovedu");

        _spriteRenderer.sprite = AktivniBrana;
        _aktivniBrana = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _spriteRenderer.sprite = DefaultBrana;
        _aktivniBrana = false;
        ZavriKnihu();
    }

    private void ZavriKnihu()
    {
        ResetniKnihu();
        Kniha.SetActive(false);
    }
}
