using Scripts.UI;
using UnityEngine;

namespace Scripts.Game.Level_Dynamics
{
    public class KeyBehaviour : MonoBehaviour
    {
        public int randomInt;
        public KeyHandler keyHandler;
        private HUDBehaviour HUDBehaviourObject;

        // Enum for assigning door colour
        public enum eKeyType
        {
            RED,
            BLUE,
            YELLOW
        };

        public eKeyType keyType;

        // Use this for initialization
        void Start()
        {
            // Random number for assigning key colour.
            randomInt = Random.Range(1, 4);
            AssignColour();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                if (keyType == eKeyType.RED)
                {
                    keyHandler.gameObject.GetComponent<KeyHandler>().redKeys++;
                    gameObject.GetComponent<Renderer>().enabled = false;
                }

                // If collected the key needs to disappear, but cannot delete as needed to check colour later on.
                gameObject.GetComponent<Renderer>().enabled = false;

                if (keyType == eKeyType.BLUE)
                {
                    keyHandler.gameObject.GetComponent<KeyHandler>().blueKeys++;
                    gameObject.GetComponent<Renderer>().enabled = false;
                    //Destroy(gameObject);
                }

                if (keyType == eKeyType.YELLOW)
                {
                    keyHandler.gameObject.GetComponent<KeyHandler>().yellowKeys++;
                    gameObject.GetComponent<Renderer>().enabled = false;
                    //Destroy(gameObject);
                }
            }
        }

        // Method to assign the colour of the key by a random value so is different every time.
        public void AssignColour()
        {
            switch (randomInt)
            {
                case 1:
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    keyType = eKeyType.RED;
                    break;

                case 2:
                    gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    keyType = eKeyType.BLUE;
                    break;

                case 3:
                    gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                    keyType = eKeyType.YELLOW;
                    break;

                default:
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                    keyType = eKeyType.RED;
                    break;
            }
        }
    }
}