using UnityEngine;
using System.Collections;

public class BlankSight : MonoBehaviour
{
    [SerializeField]
    private BlankMind mind;                     // The BlankMind of this Blank.
    [SerializeField]
    private bool checkingLOS;                   // Is the CheckLineOfSight Coroutine running?
    [SerializeField]
    private bool inVisionCone;                  // Is tha target inside of the vision cone collider?
    private AudioSource aSrc;
    
    void Awake()
    {
        mind = GetComponentInParent<BlankMind>();
        aSrc = GetComponentInChildren<AudioSource>();
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            if (other.CompareTag(Tags.Player) && other.GetComponent<CharacterController>().isDead == false)
            {
                Debug.DrawLine(mind.Head.position, other.transform.Find("Head").position);
                if (Physics.Linecast(mind.Head.position, other.transform.Find("Head").position, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore))
                {
                    if (!checkingLOS)
                        StartCoroutine(CheckLineOfSight());
                }
                else
                    mind.canSeeYou = true;
                inVisionCone = true;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bip")
        {
            if (mind.state != BlankMind.State.Chasing && other.GetComponent<CharacterController>().isDead == false)
            {
                if (!Physics.Linecast(mind.Head.position, other.transform.Find("Head").position, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore))
                    aSrc.Play();
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            if (!checkingLOS)
                StartCoroutine(CheckLineOfSight());
        }
        inVisionCone = false;
    }
    
    public IEnumerator CheckLineOfSight()
    {
        checkingLOS = true;
        yield return new WaitForSeconds(2.0f);
        if (inVisionCone == false)
            StartIdle();
        else if (mind.target.transform.Find("Head") != null)
        {
            if (Physics.Linecast(mind.Head.position, mind.target.transform.Find("Head").position, GameManager.Instance.ObstacleAvoidanceLayerMask))
                StartIdle();
        }
        else
            mind.canSeeYou = true;
        checkingLOS = false;
    }
    
    void StartIdle()
    {
        mind.canSeeYou = false;
        mind.StartCoroutine(mind.IdleWait());
    }
}