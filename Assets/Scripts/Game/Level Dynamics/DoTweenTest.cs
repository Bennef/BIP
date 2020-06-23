using DG.Tweening;
using UnityEngine;

public class DoTweenTest : MonoBehaviour
{
    public bool isOn, isMoving;
    public Rigidbody cubeB;
    public Transform endPos;
    public float moveTime;
    public Light platformLight;
        
    // Use this for initialization
    void Start()
    {
        platformLight = GetComponentInChildren<Light>();
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        if (isOn)
        {
            cubeB.DOMove(endPos.position, moveTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            platformLight.range = 20;
        }
        else
            platformLight.range = 8;
    }
    
    private void FixedUpdate()
    {
        if (isOn && !isMoving)
        {
            isMoving = true;  // So this only happens once.
            cubeB.DOMove(endPos.position, moveTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            platformLight.range = 20;
        }
    }
}