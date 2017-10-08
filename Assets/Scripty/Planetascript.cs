using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetascript : MonoBehaviour {
    public Transform UICasovacePrefab;

    // UI Casovac
    private GameObject _rucickaCasovace;
    private float _horniMezRucicky = 349f;
    private float _dolniMezRucicky = 222f;
    private float _rozsahRucicky;

    void Start () {
        // Nastaveni UI pro vsechny planety
        Instantiate(UICasovacePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject.Find("UI(Clone)").GetComponent<Canvas>().worldCamera = Camera.main;

        // Inicializace
        _rucickaCasovace = GameObject.FindGameObjectWithTag("RucickaCasovace");
        _rozsahRucicky = _horniMezRucicky - _dolniMezRucicky;
    }
	
	void Update () {
        _rucickaCasovace.transform.localPosition = new Vector3(_rucickaCasovace.transform.localPosition.x,
                                               _horniMezRucicky - (GameState.Instance.ProcentoDne * _rozsahRucicky),
                                               _rucickaCasovace.transform.localPosition.z);

        if (GameState.Instance.ProcentoDne >= 0.9f)
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.4f);
        }
        else if (GameState.Instance.ProcentoDne >= 0.75f)
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.65f);
        }
        else if (GameState.Instance.ProcentoDne >= 0.6f)
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.85f);
        }
        else
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 1f);
        }

        // Konec dne!
        if (GameState.Instance.ProcentoDne >= 1)
        {
            GameState.Instance.KonecDne();
            _rucickaCasovace.transform.localPosition = new Vector3(_rucickaCasovace.transform.localPosition.x,
                                                   _horniMezRucicky,
                                                   _rucickaCasovace.transform.localPosition.z);
        }
    }
}
