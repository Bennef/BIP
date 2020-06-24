using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class Zorb : MonoBehaviour
    {
        private AudioSource _aSrc;
        public AudioClip boing1, boing2, boing3;
        float randomNumber;

        // Use this for initialization
        void Start() => _aSrc = GetComponent<AudioSource>();

        void OnCollisionEnter(Collision collision)
        {
            randomNumber = Random.Range(0, 1f);
            if (randomNumber < 0.33f)
                PlayBoingSound(boing1);
            else if (randomNumber > 0.66f)
                PlayBoingSound(boing2);
            else
                PlayBoingSound(boing3);
        }

        public void PlayBoingSound(AudioClip boingsound) => _aSrc.PlayOneShot(boingsound);
    }
}