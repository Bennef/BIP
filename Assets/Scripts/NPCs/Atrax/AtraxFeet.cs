using UnityEngine;

namespace Scripts.NPCs.Atrax
{
    public class AtraxFeet : MonoBehaviour
    {
        private AudioSource _aSrc;
        public Transform sparkPrefab;

        // Use this for initialization
        void Start() => _aSrc = GetComponent<AudioSource>();

        void OnCollisionEnter(Collision collision)
        {
            // This is for making sparks appear when Atrax hits the floor.
            //if (collision.gameObject.name == "Hex Floor")
            //{
            //aSrc.Play();
            //}

            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(sparkPrefab, pos, rot);
        }
    }
}