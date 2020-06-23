using UnityEngine;

public class CharacterSoundManager : MonoBehaviour 
{
    private AudioSource aSrc;

    public AudioClip jump, hit, death, deathFromFall, EMP, doubleJump, boost;
    
    void Awake() => aSrc = gameObject.GetComponent<AudioSource>();

    public void PlayClip(AudioClip clip)
	{
        // Set the clip to EMP soud.
        aSrc.clip = clip;

        // Play the sound.
        if (!aSrc.isPlaying)
            aSrc.Play();
	}
}