using System.Collections;
using UnityEngine;

public class GlassPlatform : MonoBehaviour
{
    public bool platformMoving, platformIn, platformOut;
    public PressableSwitch pad;
    public Transform platformToMove, inPos, outPos;
    public AudioSource aSrc;
    public AudioClip moveOutClip, moveInClip;
    
    private void Start()
    {
        platformToMove.position = inPos.position;
        aSrc = platformToMove.gameObject.GetComponent<AudioSource>();
        platformIn = true;
        platformOut = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!platformMoving)
        {
            if (pad.switchDown && platformIn)
                StartCoroutine(MovePlatformOut());
            else if (platformOut && !platformIn && !platformMoving && !pad.switchDown)
                StartCoroutine(MovePlatformIn());
        }
	}
    
    IEnumerator MovePlatformOut()
    {
        platformMoving = true;
        StartCoroutine(MovePlatformToPosition(outPos.position, 1f));
        aSrc.clip = moveOutClip;
        aSrc.Play();
        yield return new WaitForSeconds(1.5f);
        platformIn = false;
        platformOut = true;
        platformMoving = false;
    }
    
    IEnumerator MovePlatformIn()
    {
        platformMoving = true;
        StartCoroutine(MovePlatformToPosition(inPos.position, 1f));
        aSrc.clip = moveInClip;
        aSrc.Play();
        yield return new WaitForSeconds(1f);
        platformIn = true;
        platformOut = false;
        platformMoving = false;
    }
    
    IEnumerator MovePlatformToPosition(Vector3 newPosition, float moveTime)
    {
        float elapsedTime = 0;
        Vector3 startingPos = platformToMove.position;
        while (elapsedTime < moveTime)
        {
            platformToMove.position = Vector3.Lerp(startingPos, newPosition, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, elapsedTime)));
            elapsedTime += Time.deltaTime / moveTime;
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
    }
}