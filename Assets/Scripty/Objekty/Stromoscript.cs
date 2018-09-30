using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coords;
using Assets.Scripty;
using Pada1.Xml.Serializer.Utils;
using UnityEditor;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Poctoscript))]
public class Stromoscript : MonoBehaviour {
    private Animator _animator;
    private bool _moznoSekat = false;
    public AudioClip chop1;
    public AudioClip chop2;
    public AudioClip chop3;
    private List<AudioClip> _sounds;
    private GameObject _hrac;
    public PolarCoord PolarStromu;
    private Poctoscript _poctoscript;
    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultniStrom;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
        _sounds = new List<AudioClip> {chop1, chop2, chop3};
        _poctoscript = GetComponent<Poctoscript>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultniStrom = _spriteRenderer.sprite;
        PolarStromu = new CartesianCoord(transform.position.x, transform.position.y).ToPolar();
    }

    private void Update() {
        transform.position = PolarStromu.ToCartesian().ToVector3();

        if (_hrac != null && _moznoSekat) {
            var u = transform.localPosition;
            var v = _hrac.transform.localPosition;

            float det = u.x * v.y - v.x * u.y;

            _hrac.GetComponent<SpriteRenderer>().flipX = det > 0;
        }

        if (_poctoscript && _poctoscript.Kapacita > 0) {
            _animator.enabled = true;
            _spriteRenderer.sprite = _defaultniStrom;
        } else {
            ZrusNapovedu();
            _animator.enabled = false;
            _spriteRenderer.sprite = Resources.Load<Sprite>("Sprity/suchy parez");

            // TODO: FUJ! Oprava pro moc velkou planetu
            if (SceneManager.GetActiveScene().name == "planet3") {
                PolarStromu.R = 1.22f;
            }
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerEnter2D(Collider2D collision) {
        _hrac = collision.gameObject;
        _hrac.GetComponent<PlayerController>().NastavCil(this.gameObject);
        _moznoSekat = true;
    }

    public void Seknuto() {
        if (_moznoSekat) {
            ZrusNapovedu();

            GameState.Instance.Inventar.PridejDoVolnehoSlotu(Materialy.Drevo, 1);
            _poctoscript.Kapacita--;
            _animator.SetTrigger("Chop");
            var rnd = new System.Random();
            int index = rnd.Next(_sounds.Count - 1);
            var sound = _sounds[index];

            GameState.Instance.AudioManager.ZahrajZvuk(sound);
        }
    }

    private void ZrusNapovedu() {
        var napoveda = GetComponentInChildren<Napovedascript>();
        if (napoveda != null)
        {
            Destroy(napoveda.gameObject);
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerExit2D(Collider2D collision) {
        if (_hrac) {
            _hrac.GetComponent<PlayerController>().ZrusCil();
            _hrac = null;
            _moznoSekat = false;
        }
    }

    public void OnDrawGizmos() {
        if (_poctoscript) {
            var style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 20;
            Handles.Label(transform.position, string.Format("{0}", _poctoscript.Kapacita), style);
        }

        if (_moznoSekat) {
            Handles.color = Color.green;
        } else {
            Handles.color = Color.red;
        }
        Handles.DrawSolidDisc(transform.position, Vector3.back, 0.03f);
    }
}