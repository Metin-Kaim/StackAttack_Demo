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
        public UnityAction onLevelFailed;
        public UnityAction onGameStarted;
        public Func<bool> onGetIsGameStarted;
        public UnityAction<string> onLoadScene;

        private bool IsGameStarted;
        private int currentLevel;

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
            IsGameStarted = false;
            Time.timeScale = 1;
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

        private void Start()
        {
            currentLevel = PlayerPrefs.GetInt("Level", 1);
        }
    }
}