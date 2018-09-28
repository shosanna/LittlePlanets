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
    private List<AudioClip> _sounds;
    public AudioClip trh1;
    public AudioClip trh2;
    public AudioClip trh3;

    public int? DenZasazeni = null;
    public Den.Cas? CasZasazeni = null;


    // Use this for initialization
    void Start() {
        var slunecnice = Resources.LoadAll<Sprite>("Sprity/slunecnice");
        _crop1 = slunecnice[0];
        _crop2 = slunecnice[1];
        _crop3 = slunecnice[2];
        _sounds = new List<AudioClip> { trh1, trh2, trh3 };
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

    public void ZasadNeboSklid() {
        if (!JeZasazeno()) {
            DenZasazeni = GameState.Instance.Den();
            CasZasazeni = Den.Ted();
            State = 1;
        } else {
            if (State == 3) {
                GameState.Instance.Inventar.PridejDoVolnehoSlotu(Materialy.Slunecnice, 1);

                var rnd = new System.Random();
                int index = rnd.Next(_sounds.Count - 1);
                var sound = _sounds[index];

                GameState.Instance.AudioManager.ZahrajZvuk(sound);
                Destroy(gameObject);
                _hrac.GetComponent<PlayerController>().ZrusCil();
            }
        }
    }

    public void ZkontrolujCrop() {
        if (JeZasazeno()) {
            Transform napoveda = transform.Find("NapovedaCrop");
            if (napoveda)
            {
                Destroy(napoveda.gameObject);
            }

            if ((GameState.Instance.Den() - DenZasazeni == 1) && (Den.Ted() == CasZasazeni))
            {
                State = 2;
            }
            else if ((GameState.Instance.Den() - DenZasazeni >= 2) && (Den.Ted() == CasZasazeni))
            {
                State = 3;
            }
        }
    }

    private bool JeZasazeno() {
        return DenZasazeni != null && CasZasazeni != null && State != 0;
    }
}