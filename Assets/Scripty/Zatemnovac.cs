using UnityEngine;

[ExecuteInEditMode]
public class Zatemnovac : MonoBehaviour
{
    public Material Material;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (Material != null)
            Graphics.Blit(src, dst, Material);
    }
}
