using Scripts.Game.Level_Dynamics;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class FanForce : MonoBehaviour
    {
        public float force;                  // The force of the fan.
        public bool isOn;                    // True when the fan is on.
        public RotatingObject fanBlades;     // THe fan blades' rotation script so we can stop the rotation.
        public AudioSource aSrc;

        void Start() => aSrc = GetComponent<AudioSource>();

        void Update()
        {
            // If the fan is on...
            if (isOn)
            {
                fanBlades.shouldRotate = true; // Rotate the blades.

                // If the fan sound is not already playing...
                if (!aSrc.isPlaying)
                    aSrc.Play(); // Play the fan sound.
            }
            else
            {
                fanBlades.shouldRotate = false; // Stop the blades.

                // If the fan sound is playing...
                if (!aSrc.isPlaying)
                    aSrc.Stop(); // Stop the fan sound.
            }
        }

        void OnTriggerStay(Collider col)
        {
            // If the player is stepping on this pad or a moveable object is put on it.
            if (isOn && col.gameObject.GetComponent<Rigidbody>() != null)
            {
                // Add the force.
                if (col.gameObject.CompareTag("Zorb"))
                    col.transform.GetComponent<Rigidbody>().AddForce(transform.forward * force * Time.deltaTime / 40);
                else
                    col.transform.GetComponent<Rigidbody>().AddForce(transform.forward * force * Time.deltaTime);
            }
        }
    }
}