using UnityEngine;
using System.Collections;
using Scripts.UI;
using Scripts.Game;
using Scripts.Game.Game_Logic;

namespace Scripts.Game.Level_Dynamics
{
    public class PortalBehaviour : MonoBehaviour
    {
        public string TargetLevelName;

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToBlack");
                StartCoroutine("WaitForFadeCo");
            }
        }

        IEnumerator WaitForFadeCo()
        {
            yield return new WaitForSeconds(1f);
            LocalSceneManager.Instance.LoadScene(TargetLevelName);
        }
    }
}