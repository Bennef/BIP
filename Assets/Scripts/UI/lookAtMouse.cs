using UnityEngine;

public class lookAtMouse : MonoBehaviour
{
    
    private Camera _cam;
    
    void Start() => _cam = Camera.main;

    void OnGUI()
    {
        Vector3 point = new Vector3();
        _ = Event.current;
        Vector2 mousePos = new Vector2
        {

            // Get the mouse position from Event.
            // Note that the y position from Event is inverted.
            x = Input.mousePosition.x * 5000,
            y = Input.mousePosition.y * 3000
        };
        point = _cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _cam.nearClipPlane));
        // Make CoBot look at Bip.
        transform.LookAt(point);
    }
}