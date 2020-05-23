using System.Collections;
using UnityEngine;

public class LaserSequence : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public Transform beam;
    public float delay, offTime, onTime;
    public Coroutine coroutine;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Start()
    {
        beam = transform.Find("plasma_beam_red");
    }
    // --------------------------------------------------------------------
    public void StartTheSequence()
    {
        coroutine = StartCoroutine(Sequence());
    }
    // --------------------------------------------------------------------
    public IEnumerator Sequence()
    {
        yield return new WaitForSeconds(delay);
        beam.gameObject.SetActive(false);
        yield return new WaitForSeconds(offTime);
        beam.gameObject.SetActive(true);
        yield return new WaitForSeconds(onTime);
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
