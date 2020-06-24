using Assets.Scripts.Game.Puzzles;
using System.Collections;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class MexicanWaveGlass : MonoBehaviour
    {
        public bool sequenceRunning;
        public PressableSwitch pad;
        public Transform glass1, glass2, glass3, glass4, glass5, inPos1, inPos2, inPos3, inPos4, inPos5, outPos1, outPos2, outPos3, outPos4, outPos5;
        public AudioSource aSrc1, aSrc2, aSrc3, aSrc4, aSrc5;
        public AudioClip moveInClip, moveOutClip;

        void Start()
        {
            glass1.position = inPos1.position;
            glass2.position = inPos2.position;
            glass3.position = inPos3.position;
            glass4.position = inPos4.position;
            glass5.position = inPos5.position;
            aSrc1 = glass1.gameObject.GetComponent<AudioSource>();
            aSrc2 = glass1.gameObject.GetComponent<AudioSource>();
            aSrc3 = glass1.gameObject.GetComponent<AudioSource>();
            aSrc4 = glass1.gameObject.GetComponent<AudioSource>();
            aSrc5 = glass1.gameObject.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (pad.hasBeenPressed && !sequenceRunning)
                StartCoroutine(MexicanWave());
        }

        IEnumerator MexicanWave()
        {
            sequenceRunning = true;
            StartCoroutine(MovePlatformOut(glass1, outPos1));
            aSrc1.clip = moveOutClip;
            aSrc1.Play();
            yield return new WaitForSeconds(1f);
            StartCoroutine(MovePlatformOut(glass2, outPos2));
            aSrc2.clip = moveOutClip;
            aSrc2.Play();
            yield return new WaitForSeconds(1f);
            StartCoroutine(MovePlatformIn(glass1, inPos1));
            aSrc1.clip = moveInClip;
            aSrc1.Play();
            StartCoroutine(MovePlatformOut(glass3, outPos3));
            aSrc3.clip = moveOutClip;
            aSrc3.Play();
            yield return new WaitForSeconds(1f);
            StartCoroutine(MovePlatformIn(glass2, inPos2));
            aSrc2.clip = moveInClip;
            aSrc2.Play();
            StartCoroutine(MovePlatformOut(glass4, outPos4));
            aSrc4.clip = moveOutClip;
            aSrc4.Play();
            yield return new WaitForSeconds(1f);
            StartCoroutine(MovePlatformIn(glass3, inPos3));
            aSrc3.clip = moveInClip;
            aSrc3.Play();
            StartCoroutine(MovePlatformOut(glass5, outPos5));
            aSrc5.clip = moveOutClip;
            aSrc5.Play();
            yield return new WaitForSeconds(1f);
            StartCoroutine(MovePlatformIn(glass4, inPos4));
            aSrc4.clip = moveInClip;
            aSrc4.Play();
            yield return new WaitForSeconds(1f);
            StartCoroutine(MovePlatformIn(glass5, inPos5));
            aSrc5.clip = moveInClip;
            aSrc5.Play();
            yield return new WaitForSeconds(1f);
            sequenceRunning = false;
        }

        IEnumerator MovePlatformOut(Transform glass, Transform outPos)
        {
            StartCoroutine(MovePlatformToPosition(glass, outPos.position, 1f));
            yield return new WaitForSeconds(1.5f);
        }

        IEnumerator MovePlatformIn(Transform glass, Transform inPos)
        {
            StartCoroutine(MovePlatformToPosition(glass, inPos.position, 1f));
            yield return new WaitForSeconds(1f);
        }

        IEnumerator MovePlatformToPosition(Transform glass, Vector3 newPosition, float moveTime)
        {
            float elapsedTime = 0;
            Vector3 startingPos = glass.position;
            while (elapsedTime < moveTime)
            {
                glass.position = Vector3.Lerp(startingPos, newPosition, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, elapsedTime)));
                elapsedTime += Time.deltaTime / moveTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}