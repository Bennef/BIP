﻿using UnityEngine;

namespace Scripts.Game.Game_Logic
{

    // Singleton Principle. SceneManager is static and creates a reference to itself. Allowing it to be called from any object with SceneManager.Instance.
    public class LocalSceneManager : MonoBehaviour
    {
        public static LocalSceneManager Instance { get; private set; }

        public void Awake()
        {
            // Singleton Principle. SceneManager is static and creates a reference to itself. Allowing it to be called from any object with SceneManager.Instance.
            if (Instance == null)
                Instance = this;
            if (Instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(this);
        }

        public void LoadScene(string levelName)
        {
            // This is basically all we need.
            // If no levelName is passed through, go to Main Menu
            // otherwise, we load the appropriate level
            if (string.IsNullOrEmpty(levelName))
            {
                //Application.LoadLevel("Main_Menu");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu Portfolio");    // Added this for Cinema Director - BF
            }
            else
            {
                GameManager.Instance.CurrentScene = levelName;
                //Application.LoadLevel(levelName);
                UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);    // Added this for Cinema Director - BF
            }
        }
    }
}