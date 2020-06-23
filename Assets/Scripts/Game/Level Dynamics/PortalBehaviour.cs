using UnityEngine;
using System.Collections;

public class PortalBehaviour : MonoBehaviour 
{
    public string TargetLevelName;
   
    void OnTriggerStay(Collider other)
	{
		if (other.CompareTag(Tags.Player))
		{
            GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToBlack");
            StartCoroutine("WaitForFadeCo");
		}
	}
    
    IEnumerator WaitForFadeCo()
    {
        yield return new WaitForSeconds(1f);
        LocalSceneManager.Instance.LoadScene(TargetLevelName);
    }
}