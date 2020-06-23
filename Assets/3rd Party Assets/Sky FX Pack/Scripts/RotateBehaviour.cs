using UnityEngine;

public class RotateBehaviour : MonoBehaviour 
{
    [SerializeField] private Vector3 RotationAmount;
		
	void Update() => transform.Rotate(RotationAmount * Time.deltaTime);
}