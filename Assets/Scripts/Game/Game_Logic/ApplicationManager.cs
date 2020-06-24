using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class ApplicationManager : MonoBehaviour
    {
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
        }
    }
}