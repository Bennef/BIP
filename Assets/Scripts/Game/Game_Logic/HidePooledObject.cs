using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class HidePooledObject : MonoBehaviour
    {
        void OnEnable()
        {
            //Calls 'HideObject' after 2 seconds
            Invoke("HideObject", 2f);
        }

        //method to deactivte the object
        void HideObject() => gameObject.SetActive(false);

        //Gets called on deactivation
        void OnDisable() => CancelInvoke();
    }
}