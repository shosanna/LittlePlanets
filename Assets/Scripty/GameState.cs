using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public bool RunTutorial = true;
    public bool BranaUspesneVytocena = false;

    private static GameState _instance = null;
    private float _ubehlyCas = 0f;
    private float _odpoledne = 6f;
    private float _vecer = 8f;
    private float _noc = 10f;
    private float _delkaDne = 12f;

    public static GameState Instance
    {
        get { return _instance; }
    }

    void Start () {
        // Run the tutorial only for the first time
		if (RunTutorial == true)
        {
            Fungus.Flowchart.BroadcastFungusMessage("SpustIntroNapovedu");
        }

        GetComponent<AudioSource>().Play();
	}

    void Awake()
    {
        // Immidiately destroy any other instance of game state - it must be only one (the first one)
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            _instance = this;
        }

        // Make the first game state live forever :)
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        _ubehlyCas += Time.deltaTime;

        if (_ubehlyCas >= _noc)
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.2f);
        } else if (_ubehlyCas >= _vecer)
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.5f);
        } else if (_ubehlyCas >= _odpoledne)
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.7f);
        } else
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 1f);
        }

        if (_ubehlyCas >= _delkaDne)
        {
            _ubehlyCas = 0;
        }

    }
}
