﻿using UnityEngine;

public class Conveyor : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public bool isOn;
    public Transform targetPos, resetPos;
    public float speed;
    protected new Rigidbody rigidbody;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    // --------------------------------------------------------------------
    private void FixedUpdate()
    {
        if (isOn)
        {
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * speed));
        }
    }
    // --------------------------------------------------------------------
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collider>().gameObject.name == "Reset Collider")
        {
            this.transform.position = resetPos.position;
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}