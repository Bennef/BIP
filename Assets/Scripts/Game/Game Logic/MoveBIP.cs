using UnityEngine;

public class MoveBIP : MonoBehaviour
{
    private Transform bip;
    public Vector3 standingPos;
    
    void Awake() => bip = GameObject.Find("Bip").GetComponent<Transform>();

    void Update() => bip.position = standingPos;
}