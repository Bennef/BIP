using UnityEngine;
using System.Collections;

public class CharacterSoundManager : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    AudioSource aSrc;
	Animator anim;
	PlayerStates state;
	CharacterController controller;

    public AudioClip jump, hit, death, deathFromFall, EMP, doubleJump, boost;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Awake()
	{
        aSrc = gameObject.GetComponent<AudioSource>();
		anim = gameObject.GetComponent<Animator>();
		state = gameObject.GetComponent<PlayerStates>();
		controller = gameObject.GetComponent<CharacterController>();
	}
    // --------------------------------------------------------------------
    public void PlayClip(AudioClip clip)
	{
        // Set the clip to EMP soud.
        aSrc.clip = clip;

        // Play the sound.
        if (!aSrc.isPlaying)
        {
            aSrc.Play();
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
