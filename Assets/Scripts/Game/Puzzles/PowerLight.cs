using UnityEngine;

public class PowerLight : MonoBehaviour
{
    private Light _theLight;
    private PowerTerminal _terminal;
    private bool _shouldBeLit;

    // Use this for initialization
    void Start() => _theLight.enabled = false;
    
    private void Update()
    {
        if (_shouldBeLit)
            _theLight.enabled = true;
        else
            _theLight.enabled = false;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Terminal"))
        {
            _terminal = other.gameObject.GetComponent<PowerTerminal>();
            if (_terminal.isPowered)
                _shouldBeLit = true;
            else
                _shouldBeLit = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Terminal"))
        {
            _terminal = other.gameObject.GetComponent<PowerTerminal>();
            if (!_terminal.isPowered)
                _shouldBeLit = false;
        }
    }
}