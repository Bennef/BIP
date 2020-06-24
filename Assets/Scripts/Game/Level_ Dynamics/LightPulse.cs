using UnityEngine;

namespace Scripts.Game.Level_Dynamics
{
    public class LightPulse : MonoBehaviour
    {
        [SerializeField] private float pulseRange = 6.0f;
        [SerializeField] private float pulseSpeed = 2.0f;
        [SerializeField] private float pulseMinimum = 2.0f;
        private Light _theLight;

        // Use this for initialization
        void Start() => _theLight = gameObject.GetComponent<Light>();

        // Update is called once per frame
        void Update() => Pulse();

        // Give the blue light a pulse.
        void Pulse() => _theLight.range = pulseMinimum + Mathf.PingPong(Time.time * pulseSpeed, pulseRange - pulseMinimum);
    }
}