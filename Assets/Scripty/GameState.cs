using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    // Core
    private static GameState _instance = null;

    // Tutorial
    public bool RunTutorial = true;

    // Zvuk brany
    public bool BranaUspesneVytocena = false;

    // Casovac
    private float _ubehlyCas = 0f;
    private float _delkaDne = 200f;

    public float ProcentoDne = 0f;

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
        GetComponent<AudioSource>().Play();
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
        ProcentoDne = (float)Math.Round(_ubehlyCas / _delkaDne, 2);
    }

    public void KonecDne()
    {
        _ubehlyCas = 0;
    }
}
