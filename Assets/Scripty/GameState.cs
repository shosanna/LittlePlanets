using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameState : MonoBehaviour {
    // Core
    private static GameState _instance = null;

    // Tutorial
    public bool RunTutorial = true;

    // Zvuky
    public bool BranaUspesneVytocena = false;
    private AudioSource _audioSource;
    private AudioSource _audioSource2;

    // Casovac
    private float _ubehlyCas = 0f;
    private float _delkaDne = 100f;
    private float _procentoDne = 0f;

    public static GameState Instance
    {
        get { return _instance; }
    }

    void Start () {
        // Tutorial se spousti jen prvne
        if (RunTutorial == true)
        {
            Fungus.Flowchart.BroadcastFungusMessage("SpustIntroNapovedu");
        }

        _audioSource = GetComponents<AudioSource>()[0];
        _audioSource2 = GetComponents<AudioSource>()[1];
        PustHudbu();
    }

    void Awake()
    {
        // Hned znic jine instance GameState - tento musi byt unikantni (Singleton)
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            _instance = this;
        }

        // Tento GameState se nikdy neznici
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        // Casovac
        _ubehlyCas += Time.deltaTime;
        _procentoDne = (float)Math.Round(_ubehlyCas / _delkaDne, 2);
    }

    public void NastavKonecDne()
    {
        _ubehlyCas = 0;
        PustHudbu();
    }

    public float ProcentoDne()
    {
        return _procentoDne;
    }

    public void PustHudbu()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
            _audioSource2.Play();
        }
    }

    public void ZastavHudbu()
    {
        if (_audioSource != null)
        {
            _audioSource.Stop();
        }
    }

    public void ZtlumHudbu(float okolik)
    {
        if (_audioSource != null)
        {
            _audioSource.volume = okolik;
        }
    }
}
