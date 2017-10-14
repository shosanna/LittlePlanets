using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destovac : MonoBehaviour {
    public Material Material;
    public bool Prsi = true;
    public AudioClip prsoHudba;

    void Start()
    {
        AudioSource.PlayClipAtPoint(prsoHudba, transform.position);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (Material != null && Prsi)
        {
            Graphics.Blit(src, dst, Material);
        }
    }
}
