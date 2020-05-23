﻿using System.Collections;
using UnityEngine;

public class HomingIco : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public Transform target;                // Target of cube.
    private Vector3 targetPos;
    private SphereCollider rangeCollider;    // To detect if target is in range.
    public bool isInRange;          // True when target in range.
    private Rigidbody cubeRB;
    public float speed;
    PlayerStates state;
    public Material[] mats;
    public Material red, blue, dead, green;
    Light light;
    public Vector3 startingPosition;
    AudioSource aSrc;
    public AudioClip death;
    public OverheadUITrigger overheadTrigger;
    public bool pausing, isDead, killCoStarted;
    private Health icoHealth;
    public CharacterController bip;
    private DamageByCollision damageByCollision;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void Awake()
    {
        cubeRB = GetComponent<Rigidbody>();
        rangeCollider = GetComponent<SphereCollider>();
        light = GetComponent<Light>();
        aSrc = GetComponent<AudioSource>();
        overheadTrigger = GetComponent<OverheadUITrigger>();
        damageByCollision = GetComponent<DamageByCollision>();
        state = GameObject.Find("Bip").GetComponent<PlayerStates>();
        mats[0] = red;
        mats[1] = blue;
        mats[2] = dead;
        mats[3] = green;
        icoHealth = GetComponent<Health>();
        target = GameObject.Find("Bip").transform;
    }
    // --------------------------------------------------------------------
    // Update is called once per frame
    void FixedUpdate()
    {
        if (icoHealth.value <= 0)
        {
            isDead = true;
        }

        if (bip.isDead)
        {
            isInRange = false;
        }

        if (!rangeCollider.enabled)
        {
            GetComponent<Renderer>().material = mats[3];
            light.color = Color.green;
        }

        if (!isDead)
        {
            // If target in range, move towards target.
            if (isInRange)
            {
                // Material goes red;
                GetComponent<Renderer>().material = mats[0];
                light.color = Color.red;

                // Chase Bip.
                if (cubeRB.velocity.magnitude < 10)
                {
                    cubeRB.velocity += (target.position - transform.position).normalized * speed;
                }
            }
            else if (rangeCollider.enabled)
            {
                // Material goes blue;
                GetComponent<Renderer>().material = mats[1];
                light.color = Color.blue;
            }
        }
        else
        {
            if (!killCoStarted)
            {
                GetComponent<Renderer>().material = mats[2];
                StartCoroutine(KillIco());
            }
        }
    }
    // --------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bip" && !isDead)
        {
            isInRange = true;
            if (!pausing)
            {
                pausing = true;
                cubeRB.AddForce(Vector3.up * 4000, ForceMode.Impulse);
                StartCoroutine(Pause());
            }
        }
        
    }
    // --------------------------------------------------------------------
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Bip")
        {
            isInRange = false;
        }
    }
    // --------------------------------------------------------------------
    public void ResetPosition()
    {
        overheadTrigger.hasHappened = false;
        if (cubeRB.velocity.magnitude > 0)
        {
            cubeRB.velocity = Vector3.zero;
            cubeRB.angularVelocity = Vector3.zero;
        }
        transform.position = startingPosition;
        isDead = false;
    }
    // --------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            ContactPoint contact = collision.contacts[0];
            GameObject spark = (GameObject)Instantiate(Resources.Load("vulcan_spark"));
            spark.transform.position = contact.point;
            aSrc.Play();
        }
    }
    // --------------------------------------------------------------------
    public IEnumerator Pause()
    {
        yield return new WaitForSeconds(2.0f);
        pausing = false;
    }
    // --------------------------------------------------------------------
    IEnumerator KillIco()
    {
        killCoStarted = true;
        aSrc.clip = death;
        aSrc.Play();
        GameObject explosion = (GameObject)Instantiate(Resources.Load("Explosion_002"));
        light.intensity = 0;
        explosion.transform.position = transform.position;
        damageByCollision.isOn = false;
        yield return null;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}