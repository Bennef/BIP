using UnityEngine;

public class JumpRangeDetector : MonoBehaviour
{
    [SerializeField] private AtraxAnimationController anim;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
            anim.StartJumpState();
    }
    
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
            anim.StopJumpState();
    }
}