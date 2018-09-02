using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripty;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CropController : MonoBehaviour {
    public int Index;
    public int State;

    private GameObject _hrac;
    private Sprite _crop1;
    private Sprite _crop2;
    private Sprite _crop3;

    public int? DenZasazeni = null;
    public Den.Cas? CasZasazeni = null;


    // Use this for initialization
    void Start() {
        var slunecnice = Resources.LoadAll<Sprite>("Sprity/slunecnice");
        _crop1 = slunecnice[0];
        _crop2 = slunecnice[1];
        _crop3 = slunecnice[2];
    }

    // Update is called once per frame
    void Update() {
        ZkontrolujCrop();

        var renderer = GetComponent<SpriteRenderer>();

        if (State == 0) {
            renderer.sprite = null;
        } else if (State == 1) {
            renderer.sprite = _crop1;
        } else if (State == 2) {
            renderer.sprite = _crop2;
        } else if (State == 3) {
            renderer.sprite = _crop3;
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
        if (napoveda) {
            Destroy(napoveda.gameObject);
        }
        GetComponent<SpriteRenderer>().sprite = _crop1;
        DenZasazeni = GameState.Instance.Den();
        CasZasazeni = Den.Ted();
        State = 1;
    }

    public void ZkontrolujCrop() {
        if (DenZasazeni != null && CasZasazeni != null) {
            if ((GameState.Instance.Den() - DenZasazeni == 1) && (Den.Ted() == CasZasazeni))
            {
                State = 2;
            }
            else if ((GameState.Instance.Den() - DenZasazeni == 2) && (Den.Ted() == CasZasazeni))
            {
                State = 3;
            }
            else if ((GameState.Instance.Den() - DenZasazeni >= 2))
            {
                State = 3;
            }
        }
    }
}