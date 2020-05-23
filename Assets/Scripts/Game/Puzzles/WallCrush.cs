using System.Collections;
using UnityEngine;
public class WallCrush : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public GameObject otherWall;    // A reference to the other wall.
    public GameObject endPosObj;    // The GameObject containing the end position of the wall.

    private Vector3 startPos;       // The initial position of the wall.
    private Vector3 endPos;         // The end position of the wall.

    public float inTime, outTime; // The speed at which the wall will move.
    private Rigidbody rb;
    private AudioSource aSrc;
    public AudioClip slideIn, slideOut, crush;
    // ----------------------------------------------- End Data members ------------------------------------------
    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        startPos = this.transform.position;
        endPos = endPosObj.transform.position;
        rb = GetComponent<Rigidbody>();
        aSrc = GetComponentInParent<AudioSource>();
    }
    // --------------------------------------------------------------------
    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.MovePosition(Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time * speed, 1)));
        if (this.transform.position == endPos)
        {
            StartCoroutine(MoveOut());
            StartCoroutine(PlayCrushClip());
        }
        else if (this.transform.position == startPos)
        {
            aSrc.clip = slideIn;
            aSrc.Play();
            StartCoroutine(MoveIn());
        }
    }
    // --------------------------------------------------------------------
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == Tags.Player && Vector3.Distance(otherWall.transform.position, this.transform.position) < 1f)
        {
           // fix sound!!
            GameManager.Instance.Player.GetComponent<PlayerHealth>().TakeDamage(1000);
        }
    }
    // --------------------------------------------------------------------
    public IEnumerator MoveIn()
    {
        float t = 0.0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / inTime;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
    // --------------------------------------------------------------------
    public IEnumerator MoveOut()
    {
        float t = 0.0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / outTime;
            transform.position = Vector3.Lerp(endPos, startPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
    // --------------------------------------------------------------------
    public IEnumerator PlayCrushClip()
    {
        aSrc.clip = crush;
        aSrc.Play();
        yield return new WaitForSeconds(0.5f);
        aSrc.clip = slideOut;
        aSrc.Play();
    }
    // --------------------------------------------------- End Methods --------------------------------------------
}
