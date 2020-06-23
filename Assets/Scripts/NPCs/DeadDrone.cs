using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DeadDrone : MonoBehaviour
{
    public List<GameObject> parts = new List<GameObject>();
    public SwitchOffGravity switchOffGravity;
    
    // Use this for initialization
    void OnEnable()
    {
        GameObject sparks = (GameObject)Instantiate(Resources.Load("Teleport Sparks"));
        GameObject explosion = (GameObject)Instantiate(Resources.Load("Explosion"));
        explosion.transform.position = this.transform.position;
        explosion.gameObject.transform.SetParent(this.transform);
        sparks.gameObject.transform.SetParent(this.transform);
        sparks.transform.position = this.transform.position;
        this.gameObject.AddComponent<Rigidbody>();
        
        switchOffGravity = GameObject.Find("Gravity Switch").GetComponent<SwitchOffGravity>();
        StartCoroutine(SeparatePartsAndDisableGravity());
    }
    
    void Update()
    {
        if (switchOffGravity.hasBeenCompleted)
        {
            GetComponent<Rigidbody>().useGravity = true;
            foreach (GameObject part in parts)
            {
                part.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
    
    public IEnumerator SeparatePartsAndDisableGravity()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody>().useGravity = false;
        foreach (GameObject part in parts)
        {
            part.transform.parent = null;
            part.AddComponent<Rigidbody>();
            part.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}