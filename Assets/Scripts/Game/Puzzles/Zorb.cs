using UnityEngine;

public class Zorb : MonoBehaviour
{
    private AudioSource aSrc;
    public AudioClip boing1, boing2, boing3;
    float randomNumber;
    
    // Use this for initialization
    void Start() => aSrc = GetComponent<AudioSource>();
    
    private void OnCollisionEnter(Collision collision)
    {
        randomNumber = Random.Range(0, 1f);
        if (randomNumber < 0.33f)
            PlayBoingSound(boing1);
        else if (randomNumber > 0.66f)
            PlayBoingSound(boing2);
        else
            PlayBoingSound(boing3);
    }
    
    public void PlayBoingSound(AudioClip boingsound)
    {
        aSrc.clip = boingsound;
        aSrc.Play();
    }
}