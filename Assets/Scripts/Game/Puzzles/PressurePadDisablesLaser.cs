using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class PressurePadDisablesLaser : MonoBehaviour
    {
        public bool hasBeensolved;
        public PressableSwitch pad;
        public GameObject laser, laserBody, particle;
        public BlankLaser blankLaserScript;
        public AudioSource aSrc;
        public Transform explPos;

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
}