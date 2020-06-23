using System.Collections;
using UnityEngine;

public class LightningFlash : MonoBehaviour
{
    public AudioSource aSrc;
    public Light lightningLight;
    public ParticleSystem strike;
    
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.L))
            StartCoroutine(Flash());
	}
    
    public IEnumerator Flash()
    {
        strike.Play();
        aSrc.Play();
        StartCoroutine(FadeLight(0f, 5f, 0.05f));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeLight(5f, 0f, 1f));
        StartCoroutine(FadeLight(0f, 5f, 0.05f));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeLight(5f, 0f, 1f));
    }
    
    public IEnumerator FadeLight(float fadeStart, float fadeEnd, float fadeTime)
    {
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            lightningLight.intensity = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
            yield return null;
        }
    }
}