using Scripts.Game.Game_Logic;
using Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class HUDBehaviour : MonoBehaviour
    {
        private int _healthValue;
        private int _powerValue;
        private Slider _healthSlider;
        private Slider _powerSlider;
        private Health _health;
        private PlayerPower _power;
        //private GameManager GameManagerScript; // Do we have to declare this?
        private GameObject _player;

        void Start()
        {
            _player = GameObject.Find("Bip");
            _healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
            _powerSlider = GameObject.Find("Power Slider").GetComponent<Slider>();
            //GameManagerScript = GameManager.Instance;
            _power = _player.GetComponent<PlayerPower>();
        }

        void Update()
        {
            if (_player != null)
            {
                _health = GameManager.Instance.Player.GetComponent<PlayerHealth>();
                GetHUDElementValues();
                DisplayHUDValues();
            }
        }

        // Get the information for the HUD values from the respective classes.
        public void GetHUDElementValues()
        {
            if (GameManager.Instance.Player == null)
                return;

            _power = GameManager.Instance.Player.GetComponent<PlayerPower>();
            if (_health != null)
                _healthValue = (int)_health.value;
            if (_power != null)
                _healthValue = (int)_power.value;
        }

        // Display the values on the HUD on-screen.
        public void DisplayHUDValues()
        {
            _healthSlider.value = _healthValue;
            _powerSlider.value = _powerValue;
        }
    }
}