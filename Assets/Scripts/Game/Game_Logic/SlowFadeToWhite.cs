using Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class SlowFadeToWhite : MonoBehaviour
    {
        public void OnTriggerEnter(Collider col)
        {
            GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToWhite");
            StartCoroutine(LoadMainMenuScene());
        }

        public IEnumerator LoadMainMenuScene()
        {
            yield return new WaitForSeconds(4f);
            LocalSceneManager.Instance.LoadScene("Main Menu Portfolio");
            yield return null;
        }
    }
}