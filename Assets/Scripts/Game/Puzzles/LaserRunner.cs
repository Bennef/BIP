using Assets.Scripts.Game.Puzzles;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class LaserRunner : MonoBehaviour
    {
        public bool running;
        public float timer = 0f;
        public LaserSequence[] lasers;
        public PressableSwitch pad;
        public AudioSource tickTock, music;
        public Player.CharacterController bip;

        // Update is called once per frame
        void Update()
        {
            if (running)
            {
                timer += Time.deltaTime;

                if (timer >= 15f || bip.IsDead)
                    EndSequence();
            }

            if (pad.hasBeenPressed && !running)
            {
                running = true;
                if (running)
                    RunSequence();
            }
        }

        public void EndSequence()
        {
            timer = 0f;
            running = false;
            pad.hasBeenPressed = false;
            MeshRenderer[] ary = { pad.whitePattern, pad.greyPattern };
            pad.SetPadColour(pad.whitePattern, ary);
            if (tickTock != null)
            {
                tickTock.Stop();
                music.volume = 1f;
            }

            foreach (LaserSequence laser in lasers)
            {
                laser.StopCoroutine(laser.coroutine);
                laser.beam.gameObject.SetActive(true);
            }
        }

        public void RunSequence()
        {
            if (tickTock != null)
            {
                tickTock.Play();
                music.volume = 0.4f;
            }

            foreach (LaserSequence laser in lasers)
                laser.StartTheSequence();
        }
    }
}