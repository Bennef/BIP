﻿
using UnityEngine;

public class GravityNudger : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------


    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {
        float speed = 1000;
        GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * speed);
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
