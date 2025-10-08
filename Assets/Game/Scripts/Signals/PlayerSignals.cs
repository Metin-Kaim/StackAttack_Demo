using System;
using UnityEngine;

namespace Assets.Game.Scripts.Signals
{
    public class PlayerSignals : MonoBehaviour
    {
        public static PlayerSignals Instance;

        public Func<float> onGetPlayerPositionY;

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