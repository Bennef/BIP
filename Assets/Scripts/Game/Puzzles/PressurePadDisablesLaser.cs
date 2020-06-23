using UnityEngine;

public class PressurePadDisablesLaser : MonoBehaviour
{
    public bool hasBeensolved;
    public PressableSwitch pad;
    public GameObject laser, laserBody;
    public GameObject particle;
    public BlankLaser blankLaserScript;
    public AudioSource aSrc;
    public Transform explPos;
        
    // Update is called once per frame
    void Update()
    {
		if (pad.hasBeenPressed && !hasBeensolved)
        {
            laser.SetActive(false);
            particle.SetActive(false);
            laserBody.GetComponent<Rigidbody>().isKinematic = false;
            laserBody.GetComponent<Rigidbody>().useGravity = true;
            blankLaserScript.activated = false;
            GameObject sparks = (GameObject)Instantiate(Resources.Load("Teleport Sparks"));
            GameObject explosion = (GameObject)Instantiate(Resources.Load("PlasmaExplosionEffect"));
            explosion.transform.position = explPos.position;
            sparks.transform.position = explPos.position;
            aSrc.Play();
            hasBeensolved = true;
        }
	}
}