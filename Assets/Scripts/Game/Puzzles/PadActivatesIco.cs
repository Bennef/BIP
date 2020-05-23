using UnityEngine;

public class PadActivatesIco : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public HomingIco[] homingIcos;
    public PressableSwitch pad;
    public CharacterController bip;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (bip.isDead)
        {
            pad.hasBeenPressed = false;
            foreach (HomingIco ico in homingIcos)
            {
                ico.GetComponent<SphereCollider>().enabled = false;
            }
        }

        foreach (HomingIco ico in homingIcos)
        {
            if (!ico.isDead)
            {
                if (pad.firstPattern.enabled)
                {
                    ico.GetComponent<SphereCollider>().enabled = false;
                    ico.isInRange = false;
                }
                else if (pad.secondPattern.enabled)
                {
                    ico.GetComponent<SphereCollider>().enabled = true;
                }
            }
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
