using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Scripts.Game.Puzzles
{
    public class ZorbButton : MonoBehaviour
    {
        public bool complete, canBeHit;
        public GameObject correctZorb;
        public int count = 3;
        public Text text;
        private AudioSource aSrc;

        void Start()
        {
            aSrc = GetComponent<AudioSource>();
            canBeHit = true;
        }

        void Update()
        {
            if (count <= 0)
            {
                count = 0;
                complete = true;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == correctZorb && !complete && canBeHit)
            {
                canBeHit = false;
                aSrc.Play();
                StartCoroutine(Wait());
                count--;
                text.text = count.ToString();
            }
        }

        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
            canBeHit = true;
        }
    }
}