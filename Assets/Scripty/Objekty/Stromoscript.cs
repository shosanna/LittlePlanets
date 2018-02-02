using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coords;
using Assets.Scripty;

[RequireComponent(typeof(Animator))]
public class Stromoscript : MonoBehaviour {
    private Animator _animator;
    private bool _moznoSekat = false;
    public AudioClip chop1;
    public AudioClip chop2;
    public AudioClip chop3;
    private List<AudioClip> _sounds;
    private GameObject _hrac;
    public PolarCoord PolarStromu;
    public PolarCoord PolarHrace;
    public Sprite DrevoObrazek;
    private int _kapacita = 3;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        _sounds = new List<AudioClip> { chop1, chop2, chop3 };
	}

    private void Update()
    {

        if (_hrac != null && _moznoSekat)
        {
            var u = transform.localPosition;
            var v = _hrac.transform.localPosition;

            float det = u.x * v.y - v.x * u.y;

            _hrac.GetComponent<SpriteRenderer>().flipX = det > 0;
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hrac = collision.gameObject;
        _hrac.GetComponent<PlayerController>().NastavCil(this.gameObject);
        _moznoSekat = true;
    }

    public void Seknuto()
    {
        if (_moznoSekat)
        {
            // Zruseni napovedy pro sekani
            var napoveda = GetComponentInChildren<Napovedascript>();
            if (napoveda != null)
            {
                Destroy(napoveda.gameObject);
            }

            GameState.Instance.Inventar.PridejDoVolnehoSlotu(Materialy.Drevo, 1, DrevoObrazek);
            _kapacita--;
            _animator.SetTrigger("Chop");
            var rnd = new System.Random();
            int index = rnd.Next(_sounds.Count - 1);
            var sound = _sounds[index];

            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, 1f);
        }

        if (_kapacita <= 0) {
            Destroy(gameObject);
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerExit2D(Collider2D collision)
    {
        _hrac.GetComponent<PlayerController>().ZrusCil();
        _hrac = null;
        _moznoSekat = false;
    }
}
