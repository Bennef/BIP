using UnityEngine;

// Makes an object rotate at a given speed on a given axis.
public class RotatingObject : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 eulerAngleVelocity;
    public bool shouldRotate;   // true if the thing should rotate. We can switch it off by setting this to false.

    void Start() => rb = GetComponent<Rigidbody>();

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldRotate)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}