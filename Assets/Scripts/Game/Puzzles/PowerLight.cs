using Assets.Scripts.Game.Puzzles;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class PowerLight : MonoBehaviour
    {
        private Light _theLight;
        private PowerTerminal _terminal;
        private bool _shouldBeLit;

        // Use this for initialization
        void Start() => _theLight.enabled = false;

        void Update()
        {
            if (_shouldBeLit)
                _theLight.enabled = true;
            else
                _theLight.enabled = false;
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Terminal"))
            {
                _terminal = other.gameObject.GetComponent<PowerTerminal>();
                if (_terminal.isPowered)
                    _shouldBeLit = true;
                else
                    _shouldBeLit = false;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Terminal"))
            {
                _terminal = other.gameObject.GetComponent<PowerTerminal>();
                if (!_terminal.isPowered)
                    _shouldBeLit = false;
            }
        }
    }
}