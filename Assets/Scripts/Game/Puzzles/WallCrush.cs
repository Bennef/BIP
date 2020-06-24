using Scripts.Game.Game_Logic;
using Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class WallCrush : MonoBehaviour
    {
        public GameObject otherWall;    // A reference to the other wall.
        public GameObject endPosObj;    // The GameObject containing the end position of the wall.
        private Vector3 startPos;       // The initial position of the wall.
        private Vector3 endPos;         // The end position of the wall.
        public float inTime, outTime; // The speed at which the wall will move.
        private AudioSource aSrc;
        public AudioClip slideIn, slideOut, crush;

        void Start()
        {
            startPos = transform.position;
            endPos = endPosObj.transform.position;
            aSrc = GetComponentInParent<AudioSource>();
        }

        void FixedUpdate()
        {
            if (transform.position == endPos)
            {
                StartCoroutine(MoveOut());
                StartCoroutine(PlayCrushClip());
            }
            else if (transform.position == startPos)
            {
                aSrc.clip = slideIn;
                aSrc.Play();
                StartCoroutine(MoveIn());
            }
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.collider.CompareTag(Tags.Player) && Vector3.Distance(otherWall.transform.position, transform.position) < 1f)
                GameManager.Instance.Player.GetComponent<PlayerHealth>().TakeDamage(1000);
        }

        public IEnumerator MoveIn()
        {
            float t = 0.0f;
            while (t <= 1.0)
            {
                t += Time.deltaTime / inTime;
                transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
                yield return null;
            }
        }

        public IEnumerator MoveOut()
        {
            float t = 0.0f;
            while (t <= 1.0)
            {
                t += Time.deltaTime / outTime;
                transform.position = Vector3.Lerp(endPos, startPos, Mathf.SmoothStep(0.0f, 1.0f, t));
                yield return null;
            }
        }

        public IEnumerator PlayCrushClip()
        {
            aSrc.clip = crush;
            aSrc.Play();
            yield return new WaitForSeconds(0.5f);
            aSrc.clip = slideOut;
            aSrc.Play();
        }
    }
}