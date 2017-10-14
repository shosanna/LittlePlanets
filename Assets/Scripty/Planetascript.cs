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
                                               _horniMezRucicky - (GameState.Instance.ProcentoDne() * _rozsahRucicky),
                                               _rucickaCasovace.transform.localPosition.z);

        if (Den.Noc())
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.4f);
        }
        else if (Den.Vecer())
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.65f);
            GameState.Instance.ZastavHudbu();
        }
        else if (Den.Odpoledne())
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 0.85f);
            GameState.Instance.ZtlumHudbu(0.1f);
        }
        else
        {
            Camera.main.GetComponent<Zatemnovac>().Material.SetFloat("_Magnitude", 1f);
        }
        
        // Konec dne!
        if (Den.Pulnoc())
        {
            GameState.Instance.NastavKonecDne();
            _rucickaCasovace.transform.localPosition = new Vector3(_rucickaCasovace.transform.localPosition.x,
                                                   _horniMezRucicky,
                                                   _rucickaCasovace.transform.localPosition.z);
        }
    }
}


public static class Den
{
    public enum Cas { Rano, Odpoledne, Vecer, Noc, Pulnoc };

    public static bool Rano()
    {
        if (GameState.Instance.ProcentoDne() < 0.6f)
            return true;
        return false;
    }

    public static bool Odpoledne()
    {
        if (GameState.Instance.ProcentoDne() >= 0.6f && GameState.Instance.ProcentoDne() < 0.75f)
            return true;
        return false;
    }

    public static bool Vecer()
    {
        if (GameState.Instance.ProcentoDne() >= 0.75f && GameState.Instance.ProcentoDne() < 0.9f)
            return true;
        return false;
    }

    public static bool Noc()
    {
        if (GameState.Instance.ProcentoDne() >= 0.9f)
            return true;
        return false;
    }

    public static bool Pulnoc()
    {
        if (GameState.Instance.ProcentoDne() >= 1f)
            return true;
        return false;
    }


    public static Cas Ted()
    {
        if (GameState.Instance.ProcentoDne() >= 1f)
        {
            return Cas.Pulnoc;
        }
        else if (GameState.Instance.ProcentoDne() >= 0.9f)
        {
            return Cas.Noc;
        }
        else if (GameState.Instance.ProcentoDne() >= 0.75f)
        {
            return Cas.Vecer;
        }
        else if (GameState.Instance.ProcentoDne() >= 0.6f)
        {
            return Cas.Odpoledne;
        }
        else
        {
            return Cas.Rano;
        }
    }
}

