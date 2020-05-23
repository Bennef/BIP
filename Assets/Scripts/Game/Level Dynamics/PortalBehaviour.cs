using UnityEngine;
using System.Collections;

public class PortalBehaviour : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public string TargetLevelName;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void OnTriggerStay(Collider other)
	{
		if(other.tag == Tags.Player)
		{
            GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToBlack");
            StartCoroutine("WaitForFadeCo");
		}
	}
    // --------------------------------------------------------------------
    IEnumerator WaitForFadeCo()
    {
        yield return new WaitForSeconds(1f);
        LocalSceneManager.Instance.LoadScene(TargetLevelName);
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
