using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public float speed = 15f;
    public bool on = true;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void OnCollisionStay(Collision obj)
    {
        float beltVelocity = speed * Time.deltaTime;
        obj.transform.GetComponent<Rigidbody>().velocity = beltVelocity * transform.forward;
    }
    // --------------------------------------------------------------------
    void Update()
    {
        offset += new Vector3(1f, 0f, 0f) * Time.deltaTime;
        //renderer.material.SetTextureOffset("_MainTex", offset);
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}