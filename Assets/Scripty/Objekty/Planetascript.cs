using System.Collections;
using System.Collections.Generic;
using Assets.Scripty;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Planetascript : MonoBehaviour {

    // UI Casovac
    public Transform UICasovacePrefab;
    public Transform PodvodoPrefab;
    public bool PovolitPodvod = true;
    private GameObject _rucickaCasovace;
    private float _horniMezRucicky = 371f;
    private float _dolniMezRucicky = 244f;
    private float _rozsahRucicky;

    private Destovac _destovac;

    void Start () {
        // Nacist planetu
        SaveManager.LoadPlanet(SceneManager.GetActiveScene().name);

        // Tutorial se spousti jen prvne
        if (GameState.Instance.RunTutorial)
        {
            GameState.Instance.SpustTutorial();
        }

        // Zobraz napovedotlacitka
        if (PovolitPodvod)
        {
            // Nastaveni UI pro vsechny planety
            Instantiate(PodvodoPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            GameObject.Find("Podvodnik(Clone)").GetComponent<Canvas>().worldCamera = Camera.main;
        }

        // Nastaveni UI pro vsechny planety
        Instantiate(UICasovacePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject.Find("UI(Clone)").GetComponent<Canvas>().worldCamera = Camera.main;

        // Inicializace
        _rucickaCasovace = GameObject.FindGameObjectWithTag("RucickaCasovace");
        _rozsahRucicky = _horniMezRucicky - _dolniMezRucicky;

        // Prseni
        _destovac = Camera.main.GetComponent<Destovac>();
        _destovac.ZacniMoznaPrset();
    }
	
	void Update () {
        _rucickaCasovace.transform.localPosition = new Vector3(_rucickaCasovace.transform.localPosition.x,
                                               _horniMezRucicky - (GameState.Instance.ProcentoDne() * _rozsahRucicky),
                                               _rucickaCasovace.transform.localPosition.z);
        
        // Konec dne!
        if (Den.Pulnoc())
        {
            _rucickaCasovace.transform.localPosition = new Vector3(_rucickaCasovace.transform.localPosition.x,
                                                   _horniMezRucicky,
                                                   _rucickaCasovace.transform.localPosition.z);
        }
    }
}