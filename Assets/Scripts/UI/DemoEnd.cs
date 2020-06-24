using UnityEngine;
using System.Collections;
using Scripts.Game.Game_Logic;

namespace Scripts.UI
{
    public class DemoEnd : MonoBehaviour
    {
        [Tooltip("How long will this screen remain open?")]
        public float time;

        void Start() => StartCoroutine("RestartCo");

        IEnumerator RestartCo()
        {
            yield return new WaitForSeconds(time);
            LocalSceneManager.Instance.LoadScene("Main Menu Demo");
        }
    }
}