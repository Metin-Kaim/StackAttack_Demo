using Assets.Game.Scripts.Handlers;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Signals
{
    public class LevelSignals : MonoBehaviour
    {
        public static LevelSignals Instance;

        public Func<LevelHandler> onGetCurrentLevel;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}