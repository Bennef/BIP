using UnityEngine;

public class SwipeRangeDetector : MonoBehaviour
{
    private AtraxAnimationController _anim;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
            _anim.StartSwipeState();
    }
    
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
            _anim.StopSwipeState();
    }
}