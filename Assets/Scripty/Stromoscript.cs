using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Stromoscript : MonoBehaviour {
    private Animator _animator;
    private bool _moznoSekat = false;
    public AudioClip chop1;
    public AudioClip chop2;
    public AudioClip chop3;
    private List<AudioClip> _sounds;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        _sounds = new List<AudioClip> { chop1, chop2, chop3 };
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X) && _moznoSekat)
        {
            _animator.SetTrigger("Chop");
            var rnd = new System.Random();
            int index = rnd.Next(_sounds.Count - 1);
            var sound = _sounds[index];

            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, 1f);
        }
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _moznoSekat = true;
    }

    // Kdyz se trigne kolize (s hracem)
    private void OnTriggerExit2D(Collider2D collision)
    {
        _moznoSekat = false;
    }
}
