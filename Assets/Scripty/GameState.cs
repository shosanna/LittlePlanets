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

    // Zvuk brany
    public bool BranaUspesneVytocena = false;

    // Casovac
    private float _ubehlyCas = 0f;
    private float _delkaDne = 10f;
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

        // Hudba
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
        GetComponent<AudioSource>().Play();
    }

    public void ZastavHudbu()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void ZtlumHudbu(float okolik)
    {
        GetComponent<AudioSource>().volume = okolik;
    }
}
