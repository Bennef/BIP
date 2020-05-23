using UnityEngine;
public class AlarmLight : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public float fadespeed = 2f;
    public float highIntensity = 2f;
    public float lowIntensity = 0.5f;
    public float changeMargin = 0.2f;
    public bool alarmOn; private float targetIntensity;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Awake()
    {
        GetComponent<Light>().intensity = 0f;
        targetIntensity = highIntensity;
    }
    // --------------------------------------------------------------------
    void Update()
    {
        if (alarmOn)
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, targetIntensity, fadespeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 0f, fadespeed * Time.deltaTime);
        }
    }
    // --------------------------------------------------------------------
    void CheckTargetIntensity()
    {
        if (Mathf.Abs(targetIntensity - GetComponent<Light>().intensity) < changeMargin)
        {
            if (targetIntensity == highIntensity)
            {
                targetIntensity = lowIntensity;
            }
            else
            {
                targetIntensity = highIntensity;
            }
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}