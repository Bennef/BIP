﻿using UnityEngine;

public class MoveBIP : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    private Transform bip;
    public Vector3 standingPos;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Awake ()
    {
        bip = GameObject.Find("Bip").GetComponent<Transform>();
    }
    // --------------------------------------------------------------------
    void Update()
    {
        bip.position = standingPos;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}