using UnityEngine;
using System.Collections;

public class DemoEnd : MonoBehaviour 
{
    [Tooltip("How long will this screen remain open?")]
    public float time;
    
    void Start() => StartCoroutine("RestartCo"); 
    
    IEnumerator RestartCo()
    {
        yield return new WaitForSeconds(time);
        LocalSceneManager.Instance.LoadScene("Main Menu Demo");
    }
}