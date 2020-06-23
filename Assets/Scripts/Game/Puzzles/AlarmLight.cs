using UnityEngine;

public class AlarmLight : MonoBehaviour
{ 
    [SerializeField] private float fadespeed = 2f;
    [SerializeField] private float highIntensity = 2f;
    [SerializeField] private float lowIntensity = 0.5f;
    [SerializeField] private float changeMargin = 0.2f;
    [SerializeField] private bool alarmOn;
    [SerializeField] private float targetIntensity;

    public bool AlarmOn { get => alarmOn; set => alarmOn = value; }

    void Awake()
    {
        GetComponent<Light>().intensity = 0f;
        targetIntensity = highIntensity;
    }
    
    void Update()
    {
        if (AlarmOn)
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, targetIntensity, fadespeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 0f, fadespeed * Time.deltaTime);
        }
    }
    
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
}