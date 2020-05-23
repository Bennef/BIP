using UnityEngine;
using System.Collections;

public class PlayerPower : PlayerHealth
{
    // ----------------------------------------------- Data members ----------------------------------------------

    // ----------------------------------------------- End Data members ------------------------------------------
    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    public override void TakeDamage(float _value)
    {
        isRegenerating = false;
        value -= _value;
        value = Mathf.Clamp(value, 0, maxValue);
        StartCoroutine("RegenDelayCo");
    }

    IEnumerator RegenDelayCo()
    {
        yield return new WaitForSeconds(delay);
        isRegenerating = true;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
