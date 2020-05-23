﻿using System.Collections;
using UnityEngine;

public class SlowFadeToWhite : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------


    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    public void OnTriggerEnter(Collider col)
    {
        GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToWhite");
        StartCoroutine(LoadMainMenuScene());
    }
    // --------------------------------------------------------------------
    public IEnumerator LoadMainMenuScene()
    {
        yield return new WaitForSeconds(4f);
        LocalSceneManager.Instance.LoadScene("Main Menu Portfolio");
        yield return null;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}