using UnityEngine;

namespace Scripts.Game.Level_Dynamics
{
    public class GravityNudger : MonoBehaviour
    {
        void Start()
        {
            float speed = 1000;
            GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * speed);
        }
    }
}