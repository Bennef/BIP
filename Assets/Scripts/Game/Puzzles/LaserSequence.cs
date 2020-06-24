using System.Collections;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class LaserSequence : MonoBehaviour
    {
        public Transform beam;
        public float delay, offTime, onTime;
        public Coroutine coroutine;

        void Start() => beam = transform.Find("plasma_beam_red");

        public void StartTheSequence() => coroutine = StartCoroutine(Sequence());

        public IEnumerator Sequence()
        {
            yield return new WaitForSeconds(delay);
            beam.gameObject.SetActive(false);
            yield return new WaitForSeconds(offTime);
            beam.gameObject.SetActive(true);
            yield return new WaitForSeconds(onTime);
        }
    }
}