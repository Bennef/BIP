using UnityEngine;
using System.Collections;

public class MirrorReflectionScript : MonoBehaviour
{
    private MirrorCameraScript childScript;

    void Start()
    {
        childScript = gameObject.transform.parent.gameObject.GetComponentInChildren<MirrorCameraScript>();

        if (childScript == null)
        {
            Debug.LogError("Child script (MirrorCameraScript) should be in sibling object");
        }
    }

    void OnWillRenderObject()
    {
        childScript.RenderMirror();
    }
}
