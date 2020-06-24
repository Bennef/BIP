using UnityEngine;

namespace Scripts.Image_Effects
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/PixelBoy")]
    public class PixelBoy : MonoBehaviour
    {
        public int w = 720;
        int h;

        void Update()
        {
            float ratio = Camera.main.pixelHeight / (float)Camera.main.pixelWidth;
            h = Mathf.RoundToInt(w * ratio);
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            source.filterMode = FilterMode.Point;
            RenderTexture buffer = RenderTexture.GetTemporary(w, h, -1);
            buffer.filterMode = FilterMode.Point;
            Graphics.Blit(source, buffer);
            Graphics.Blit(buffer, destination);
            RenderTexture.ReleaseTemporary(buffer);
        }
    }
}