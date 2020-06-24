using Scripts.Game.Level_Dynamics;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class ShockFloor : MonoBehaviour
    {
        public GameObject[] shockCubeArray;    // The array of all the Shock Cubes in the grid.
        public bool[] shockCubeBool;           // The array of bools to use to select the shockfloor pattern

        public int onTime;                      // The delay for the shockfloor switching on/off
        private float timer;                   // Timer for shockfloor pattern
        public float delay;                     // Time to turn off before turning on again.

        // Use this for initialization
        void Start()
        {
            int i = 0;
            foreach (Transform child in transform)
            {
                //shockCubeArray[i] = child.gameObject;                       // Adds shockfloor cube to array.
                bool shockCubeActive = shockCubeBool[i] ? true : false;     // Checks to see if shockfloor should start as on/off.
                child.GetChild(0).GetComponent<Transform>().gameObject.SetActive(shockCubeActive ? true : false);   // Assigns shockfloor cube particle effects as on/off according to the pattern in editor.
                i++;
            }
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer > onTime)
            {
                for (int i = 0; i < shockCubeBool.Length; i++)
                {
                    shockCubeArray[i].transform.GetChild(0).GetComponent<Transform>().gameObject.SetActive(shockCubeBool[i] ? false : true); // Checks current shockCubeBool state and switches the cubes to the opposite state.
                    shockCubeArray[i].transform.GetChild(1).GetComponent<Transform>().gameObject.SetActive(shockCubeBool[i] ? false : true); // Checks current shockCubeBool state and switches the cubes to the opposite state.
                    shockCubeBool[i] = !shockCubeBool[i];    // Swaps bools current states to the oppisite state.
                    if (shockCubeBool[i] == true)
                        ShockOn(shockCubeArray[i]);
                    else
                        ShockOff(shockCubeArray[i]);
                    timer = 0;    // Resets the timer.
                }
            }
        }

        public void ShockOn(GameObject shockCube) => shockCube.GetComponent<DamageByCollision>().isOn = true;

        public void ShockOff(GameObject shockCube) => shockCube.GetComponent<DamageByCollision>().isOn = false;
    }
}