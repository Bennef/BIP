using UnityEngine;

public class PowerLight : MonoBehaviour
{
    public Light light;
    public PowerTerminal terminal;
    public bool shouldBeLit;

	// Use this for initialization
	void Start()
    {
        light.enabled = false;
	}
    // --------------------------------------------------------------------
    private void Update()
    {
        if (shouldBeLit)
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
    }
    // --------------------------------------------------------------------
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Terminal")
        {
            terminal = other.gameObject.GetComponent<PowerTerminal>();
            if (terminal.isPowered)
            {
                shouldBeLit = true;
            }
            else
            {
                shouldBeLit = false;
            }
        }
    }
    // --------------------------------------------------------------------
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terminal")
        {
            terminal = other.gameObject.GetComponent<PowerTerminal>();
            if (!terminal.isPowered)
            {
                shouldBeLit = false;
            }
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
