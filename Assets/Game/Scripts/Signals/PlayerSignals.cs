using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.Signals
{
    public class PlayerSignals : MonoBehaviour
    {
        public static PlayerSignals Instance;

        public Func<float> onGetPlayerPositionY;
        public UnityAction<ModuleType> onAddModule;

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