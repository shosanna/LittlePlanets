using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Destovac : MonoBehaviour {
    public Material Material;
    public AudioClip prsoHudba;
    private bool _prsi;
    private bool _jesteNezkontrolovano = true;

    public void ZacniMoznaPrset()
    {
        System.Random rnd = new System.Random();

        if (rnd.Next(100) > 80)
        {
            _prsi = true;
        }
    }

    private void Start() {
        if (!_prsi && GameState.Instance.AudioManager != null)
        {
            GameState.Instance.AudioManager.ZmenEfektNaDefault();
            GameState.Instance.AudioManager.PustEfekt();
        }
    }

    private void Update()
    {
        if (_prsi && _jesteNezkontrolovano)
        {
            GameState.Instance.AudioManager.ZmenEfekt(prsoHudba);
            GameState.Instance.AudioManager.PustEfekt();
            _jesteNezkontrolovano = false;
        }
    }

    private void OnDestroy() {
        _prsi = false;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (Material != null && _prsi)
        {
            Graphics.Blit(src, dst, Material);
        } else
        {
            Graphics.Blit(src, dst);
        }
    }
}
