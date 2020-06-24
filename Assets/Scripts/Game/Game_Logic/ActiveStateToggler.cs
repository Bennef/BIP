using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class ActiveStateToggler : MonoBehaviour
    {
        public void ToggleActive() => gameObject.SetActive(!gameObject.activeSelf);
    }
}