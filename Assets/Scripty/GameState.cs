using Assets.Scripty;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameState : MonoBehaviour {
    // Core
    private static GameState _instance = null;

    public Inventar Inventar;

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

    public static GameState Instance {
        get { return _instance; }
    }

    void Start() {
        // Tutorial se spousti jen prvne
        if (RunTutorial == true) {
            Fungus.Flowchart.BroadcastFungusMessage("SpustIntroNapovedu");
        }


        _audioSource = GetComponents<AudioSource>()[0];
        _audioSource2 = GetComponents<AudioSource>()[1];

        Debug.Assert(_audioSource != null);
        Debug.Assert(_audioSource2 != null);

        Inventar = new Inventar();

        PustHudbu();
    }

    void Awake() {
        // Hned znic jine instance GameState - tento musi byt unikantni (Singleton)
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
       
            // Znic take vsechny napovedy - maji byt jen jednou
            var napovedy = GameObject.FindGameObjectsWithTag("Napoveda");

            foreach (var napoveda in napovedy) {
                Destroy(napoveda);
            }
            return;
        } else {
            _instance = this;
        }

        // Tento GameState se nikdy neznici
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update() {
        // Casovac
        _ubehlyCas += Time.deltaTime;
        _procentoDne = (float) Math.Round(_ubehlyCas / _delkaDne, 2);

        // Inventar
        Inventar.MujUpdate();
    }

    public void NastavKonecDne() {
        _ubehlyCas = 0;
        PustHudbu();
    }

    public float ProcentoDne() {
        return _procentoDne;
    }

    public void PustHudbu() {
        _audioSource.Play();
        _audioSource2.Play();
    }

    public void ZastavHudbu() {
        _audioSource.Stop();
    }

    public void ZtlumHudbu(float okolik) {
        _audioSource.volume = okolik;
    }
}