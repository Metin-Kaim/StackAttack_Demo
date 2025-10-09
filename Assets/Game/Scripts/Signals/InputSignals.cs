using System;
using UnityEngine;

namespace Assets.Game.Scripts.Signals
{
    public class InputSignals : MonoBehaviour
    {
        public static InputSignals Instance;

        public Func<float> onGetMouseDeltaX;
        public Func<bool> onGetIsTouching;

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