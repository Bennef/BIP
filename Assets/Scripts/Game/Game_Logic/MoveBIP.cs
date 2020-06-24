using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class MoveBIP : MonoBehaviour
    {
        private Transform bip;
        public Vector3 standingPos;

        void Awake() => bip = GameObject.Find("Bip").GetComponent<Transform>();

        void Update() => bip.position = standingPos;
    }
}