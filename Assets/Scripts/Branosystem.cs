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
        if (PlayerPrefs.GetInt("dial_success") == 1)
        {
            AudioSource.PlayClipAtPoint(ZvukUspech, Camera.main.transform.position, 0.05f);
            PlayerPrefs.DeleteKey("dial_success");
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
            PlayerPrefs.SetInt("dial_success", 1);
        
            brana.ResetniKnihu();
            Application.LoadLevel(_adresy[adresa]);
        } else
        {
            AudioSource.PlayClipAtPoint(ZvukFail, Camera.main.transform.position.normalized, 0.05f);
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