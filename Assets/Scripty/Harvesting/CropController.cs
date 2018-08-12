using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour {
    private GameObject _hrac;
    private Sprite _crop1;
    private Sprite _crop2;
    private Sprite _crop3;
    private int _denZasazeni = -999;
    private Den.Cas _casZasazeni;


    // Use this for initialization
    void Start () {
        var slunecnice = Resources.LoadAll<Sprite>("Sprity/slunecnice");
        _crop1 = slunecnice[0];
        _crop2 = slunecnice[1];
        _crop3 = slunecnice[2];
    }

    // Update is called once per frame
    void Update () {
        if ((GameState.Instance.Den() - _denZasazeni == 1) && (Den.Ted() == _casZasazeni)) {
            GetComponent<SpriteRenderer>().sprite = _crop2;
        } else if ((GameState.Instance.Den() - _denZasazeni == 2) && (Den.Ted() == _casZasazeni)) {
            GetComponent<SpriteRenderer>().sprite = _crop3;
        } 
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        _hrac = collision.gameObject;
        _hrac.GetComponent<PlayerController>().NastavCil(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        _hrac = collision.gameObject;
        _hrac.GetComponent<PlayerController>().ZrusCil();
    }

    public void Zasad() {
        Transform napoveda = transform.Find("NapovedaCrop");
        if (napoveda)
        {
            Destroy(napoveda.gameObject);
        }
        GetComponent<SpriteRenderer>().sprite = _crop1;
        _denZasazeni = GameState.Instance.Den();
        _casZasazeni = Den.Ted();
    }
}
