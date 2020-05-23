using UnityEngine;

public class lookAtMouse : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    private Camera cam;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Start()
    {
        cam = Camera.main;
    }
    // --------------------------------------------------------------------
    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = Input.mousePosition.x*5000;
        mousePos.y = Input.mousePosition.y*3000;
        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        // Make CoBot look at Bip.
        transform.LookAt(point);
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
