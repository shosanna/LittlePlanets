using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetascript : MonoBehaviour {

    // UI Casovac
    public Transform UICasovacePrefab;
    private GameObject _rucickaCasovace;
    private float _horniMezRucicky = 371f;
    private float _dolniMezRucicky = 244f;
    private float _rozsahRucicky;

    private Destovac _destovac;

    void Start () {
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