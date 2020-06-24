using UnityEngine;

namespace Scripts.Player
{
    public class CharacterSoundManager : MonoBehaviour
    {
        private AudioSource _aSrc;
        [SerializeField] private AudioClip jump, hit, death, deathFromFall, EMP, doubleJump, boost;

        public AudioClip Jump { get => jump; set => jump = value; }
        public AudioClip Hit { get => hit; set => hit = value; }
        public AudioClip Death { get => death; set => death = value; }
        public AudioClip DeathFromFall { get => deathFromFall; set => deathFromFall = value; }
        public AudioClip EMP1 { get => EMP; set => EMP = value; }
        public AudioClip DoubleJump { get => doubleJump; set => doubleJump = value; }
        public AudioClip Boost { get => boost; set => boost = value; }

        void Awake() => _aSrc = gameObject.GetComponent<AudioSource>();

        public void PlayClip(AudioClip clip)
        {
            if (!_aSrc.isPlaying)
                _aSrc.PlayOneShot(clip);
        }
    }
}