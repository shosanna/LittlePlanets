using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class Destovac : MonoBehaviour {
    public Material Material;
    public AudioClip prsoHudba;
    public bool Prsi = false;

    private AudioSource _audioSource;

    public void ZacniMoznaPrset()
    {
        System.Random rnd = new System.Random();

        if (rnd.Next(100) > 80)
        {
            Prsi = true;
        }
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = prsoHudba;
    }

    private void Update()
    {
        if (!_audioSource.isPlaying && Prsi)
        {
            _audioSource.Play();
            GameState.Instance.AudioManager.ZastavHudbu();
            Prsi = false;
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (Material != null && _audioSource.isPlaying)
        {
            Graphics.Blit(src, dst, Material);
        } else
        {
            Graphics.Blit(src, dst);
        }
    }
}
