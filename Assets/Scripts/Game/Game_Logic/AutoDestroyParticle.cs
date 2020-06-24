using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class AutoDestroyParticle : MonoBehaviour
    {
        // Creates a spark when somethin hits a laser.
        private ParticleSystem ps;

        // Use this for initialization
        void Awake() => ps = gameObject.GetComponent<ParticleSystem>();

        // Update is called once per frame
        void Update()
        {
            // If is not null.
            if (ps)
            {
                // If particle system has finished.
                if (!ps.IsAlive())
                    Destroy(gameObject);
            }
        }
    }
}