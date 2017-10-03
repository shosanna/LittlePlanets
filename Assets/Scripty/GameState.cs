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
    private float _delkaDne = 12f;


    // UI Casovac
    private GameObject _rucickaCasovace;
    private float _horniMezRucicky = 349f;
    private float _dolniMezRucicky = 222f;
    private float _rozsahRucicky;
    private float _procentoDne = 0f;

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
        _rucickaCasovace = GameObject.FindGameObjectWithTag("RucickaCasovace");
        _rozsahRucicky = _horniMezRucicky - _dolniMezRucicky;
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

        // Casovac
        _ubehlyCas += Time.deltaTime;
        _procentoDne = (float)Math.Round(_ubehlyCas / _delkaDne, 2);

        _rucickaCasovace.transform.localPosition = new Vector3(_rucickaCasovace.transform.localPosition.x,
                                                _horniMezRucicky - (_procentoDne * _rozsahRucicky),
                                                _rucickaCasovace.transform.localPosition.z);
 
        if (_procentoDne >= 0.9f)
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.4f);
        } else if (_procentoDne >= 0.75f)
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.65f);
        } else if (_procentoDne >= 0.6f)
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.85f);
        } else
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 1f);
        }
        
        // Konec dne!
        if (_procentoDne >= 1)
        {
            _ubehlyCas = 0;
            _rucickaCasovace.transform.localPosition = new Vector3(_rucickaCasovace.transform.localPosition.x,
                                                   _horniMezRucicky,
                                                   _rucickaCasovace.transform.localPosition.z);
        }

    }
}
