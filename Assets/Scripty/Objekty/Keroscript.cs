﻿using Assets.Scripty;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keroscript : MonoBehaviour {
    private Animator _animator;
    private bool _moznoTrhat = false;
    public AudioClip trh1;
    public AudioClip trh2;
    public AudioClip trh3;
    private List<AudioClip> _sounds;
    private GameObject _hrac;
    public Sprite BoruvkaObrazek;
    public bool _otrhano = false;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
        _sounds = new List<AudioClip> {trh1, trh2, trh3};
    }

    private void Update() {
        if (_hrac != null && _moznoTrhat) {
            var u = transform.localPosition;
            var v = _hrac.transform.localPosition;

            float det = u.x * v.y - v.x * u.y;

            _hrac.GetComponent<SpriteRenderer>().flipX = det > 0;
        }
    }

    public void Trhani() {
        if (_moznoTrhat && !_otrhano) {
            GameState.Instance.Inventar.PridejDoVolnehoSlotu(Materialy.Boruvka, 5, BoruvkaObrazek);

            _animator.SetTrigger("Trhani");
            var rnd = new System.Random();
            int index = rnd.Next(_sounds.Count - 1);
            var sound = _sounds[index];

            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, 1f);
            _otrhano = true;
            _hrac.GetComponent<PlayerController>().ZrusCil();
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerEnter2D(Collider2D collision) {
        if (!_otrhano) {
            _hrac = collision.gameObject;
            _hrac.GetComponent<PlayerController>().NastavCil(this.gameObject, "trhani");
            _moznoTrhat = true;
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerExit2D(Collider2D collision) {
        if (_hrac != null) {
            Debug.Assert(collision.gameObject == _hrac);

            _hrac.GetComponent<PlayerController>().ZrusCil();
            _hrac = null;
            _moznoTrhat = false;
        }
    }
}