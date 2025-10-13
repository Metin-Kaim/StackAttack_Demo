using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.Signals
{
    public class PlayerSignals : MonoBehaviour
    {
        public static PlayerSignals Instance;

        public Func<float> onGetPlayerPositionY;
        public Func<Vector2> onGetPlayerPosition;
        public UnityAction<ModuleType> onAddModule;
        public UnityAction onDecreaseHeart;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }
    }
}