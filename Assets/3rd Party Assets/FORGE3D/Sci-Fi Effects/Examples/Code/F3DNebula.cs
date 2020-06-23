using UnityEngine;

public class F3DNebula : MonoBehaviour {

	// Update is called once per frame
	void Update() {

        transform.position -= Vector3.forward * Time.deltaTime * 100;

        if (transform.position.z < -2150)
        {
            Vector3 newPos = transform.position;
            newPos.z = 2150;
            transform.position = newPos;
            transform.rotation = Random.rotation;
            transform.localScale = new Vector3(1, 1, 1) * Random.Range(100, 200);
        }
            
	}
}
