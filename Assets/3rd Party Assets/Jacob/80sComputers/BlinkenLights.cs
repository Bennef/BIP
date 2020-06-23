using UnityEngine;
using System.Collections;

public class BlinkenLights : MonoBehaviour 
{
	void Start() => StartCoroutine(Counter());

	public float Rand;
	public float LightOn;
	public bool bool1;
	void Update() 
	{
		if (Rand <= 60) 
			LightOn = 0;
		if (Rand > 60) 
			LightOn = 1;

		Renderer renderer = GetComponent<Renderer>();
		Material mat = renderer.material;
		float emission = LightOn;
		//float emission = Mathf.PingPong (Time.time, 1.0f);
		Color baseColor = Color.white; //Replace this with whatever you want for your base color at emission level '1'

		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);

		mat.SetColor ("_EmissionColor", finalColor);
	}

	IEnumerator Counter()
	{
		while (bool1 == false) 
		{
			yield return new WaitForSeconds (Random.Range (0.01f, 0.3f));
			Rand = Mathf.RoundToInt (Random.Range (0, 100));
		}
	}
}