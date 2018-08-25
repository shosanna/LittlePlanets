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
        // uspesne dialovani z predchozi sceny - zvuk
        if (GameState.Instance.BranaUspesneVytocena)
        {
            GameState.Instance.BranaUspesneVytocena = false;
        }

        for (int i = 0; i < Adresy.Length; i++)
        {
            _adresy[Adresy[i].Adresa] = Adresy[i].Scena;
        }
    }

    public void VytocBranu(string adresa, Branoscript brana)
    {
        if(_adresy.ContainsKey(adresa))
        {
            GameState.Instance.BranaUspesneVytocena = true;
            brana.ResetniKnihu();
            GameState.Instance.AudioManager.ZahrajZvuk(ZvukUspech);

            
            Application.LoadLevel(_adresy[adresa]);


        } else
        {
            GameState.Instance.AudioManager.ZahrajZvuk(ZvukFail);
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