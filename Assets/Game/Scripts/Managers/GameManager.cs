using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Game.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public UnityAction onLevelCompleted;
        public UnityAction onGameStarted;
        public Func<bool> onGetIsGameStarted;
        public UnityAction onGameOver;
        public UnityAction<string> onLoadScene;

        private bool IsGameStarted;
        private byte currentLevel;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        private void OnEnable()
        {
            onLevelCompleted += OnLevelCompleted;
            onGameStarted += OnGameStarted;
            onGetIsGameStarted += OnGetIsGameStarted;
            onLoadScene += OnLoadScene;
        }

        public void OnLoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        private bool OnGetIsGameStarted()
        {
            return IsGameStarted;
        }

        private void OnGameStarted()
        {
            IsGameStarted = true;
        }

        private void OnLevelCompleted()
        {
            currentLevel++;
            PlayerPrefs.SetInt("Level", currentLevel);
        }

        private void OnDisable()
        {
            onLevelCompleted -= OnLevelCompleted;
            onGameStarted -= OnGameStarted;
            onGetIsGameStarted -= OnGetIsGameStarted;
            onLoadScene -= OnLoadScene;
        }
    }
}