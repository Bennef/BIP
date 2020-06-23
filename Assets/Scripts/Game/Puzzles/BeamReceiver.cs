using UnityEngine;

public class BeamReceiver : MonoBehaviour
{
    public bool isHit, a, b;
    public MeshRenderer litBulb, unlitBulb;
    public Light bulbLight;
    public RaycastReflection[] beams;
    public string receiverName;
    
    // Update is called once per frame
    void LateUpdate() => CheckForHits();
 
    public void LightOn()
    {
        unlitBulb.enabled = false;
        litBulb.enabled = true;
        bulbLight.enabled = true;
    }
    
    public void LightOff()
    {
        unlitBulb.enabled = true;
        litBulb.enabled = false;
        bulbLight.enabled = false;
    }
    
    public void CheckForHits()
    {
        a = b = false;
        foreach (RaycastReflection beam in beams)
        {
            if (receiverName == "Unlit Bulb A")
            {
                if (beam.receiverAHit == true)
                {
                    if (beam.isOn == true)
                    {
                        LightOn();
                        a = true;
                    }
                }
                else if (a == false)
                    LightOff();
            }

            else if (receiverName == "Unlit Bulb B")
            {
                if (beam.receiverBHit == true)
                {
                    if (beam.isOn == true)
                    {
                        LightOn();
                        b = true;
                    }
                }
                else if (b == false)
                    LightOff();
            }
        }
    }
}