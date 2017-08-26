using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branosystem : MonoBehaviour {
    public AudioClip ZvukUspech;
    public AudioClip ZvukFail;

    public AdresaScena[] Adresy;
    private Dictionary<string, string> _adresy = new Dictionary<string, string>();

    private void Start()
    {
        for (int i = 0; i < Adresy.Length; i++)
        {
            _adresy[Adresy[i].Adresa] = Adresy[i].Scena;
        }
    }

    public void VytocBranu(string adresa, Branoscript brana)
    {
        Debug.Log(adresa);
        if(_adresy.ContainsKey(adresa))
        {
            AudioSource.PlayClipAtPoint(ZvukUspech, Camera.main.transform.position);
            brana.ResetniKnihu();
            Application.LoadLevel(_adresy[adresa]);
        } else
        {
            AudioSource.PlayClipAtPoint(ZvukFail, Camera.main.transform.position);
            brana.ResetniKnihu();
        }
    }
}

[Serializable]
public struct AdresaScena
{
    public string Adresa;
    public string Scena;
}