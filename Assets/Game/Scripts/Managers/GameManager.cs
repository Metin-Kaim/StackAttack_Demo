using Assets.Game.Scripts.Signals;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private bool IsGameStarted;

        private void OnEnable()
        {
            GameSignals.Instance.onLevelCompleted += OnLevelCompleted;
            GameSignals.Instance.onGameStarted += OnGameStarted;
            GameSignals.Instance.onGetIsGameStarted += OnGetIsGameStarted;
        }

        private bool OnGetIsGameStarted()
        {
            return IsGameStarted;
        }

        private void OnGameStarted()
        {
            IsGameStarted = true;
            print("Game Started! Handling in GameManager.");
        }

        private void OnLevelCompleted()
        {
            print("Level Completed! Handling in GameManager.");
        }

        private void OnDisable()
        {
            GameSignals.Instance.onLevelCompleted -= OnLevelCompleted;
            GameSignals.Instance.onGameStarted -= OnGameStarted;
        }
    }
}