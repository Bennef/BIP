using UnityEngine;

public class LookAtCamera : MonoBehaviour 
{
    private Camera _mainCam;
     
    void Start() => _mainCam = Camera.main;
    
    void Update()
    {
        if (_mainCam == null)
            _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        transform.LookAt(_mainCam.transform);
    }
}