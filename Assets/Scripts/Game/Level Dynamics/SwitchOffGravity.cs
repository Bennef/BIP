using UnityEngine;
using System.Collections.Generic;

public class SwitchOffGravity : MonoBehaviour
{
    public PressableSwitch pad;
    public List<GameObject> items = new List<GameObject>();
    public bool hasBeenCompleted;
    private AudioSource aSrc;
    public Transform fog;
    public List<ParticleSystem> fogList = new List<ParticleSystem>();
    public GameObject mainCam;
    
    void Start()
    {
        aSrc = GetComponent<AudioSource>();
        foreach (Transform child in fog)
        {
            fogList.Add(child.gameObject.GetComponent<ParticleSystem>());
            foreach (Transform grandChild in child)
                fogList.Add(grandChild.gameObject.GetComponent<ParticleSystem>());
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    	if (pad.hasBeenPressed && !hasBeenCompleted)
        {
            hasBeenCompleted = true;
            TurnGravityOff();
        }	
	}
    
    // find all items with Gravity as the tag.
    public void GetItems()
    {
        foreach (GameObject thing in GameObject.FindGameObjectsWithTag("Gravity"))
            items.Add(thing);
    }
    
    public void TurnGravityOff()
    {
        GetItems();
        foreach (GameObject item in items)
        {
            if (item.GetComponent<Rigidbody>() != null)
            item.GetComponent<Rigidbody>().useGravity = true;
        }
        aSrc.Play();
        foreach (ParticleSystem ps in fogList)
        {
            var main = ps.main;
            main.loop = false;
        }
    }
}