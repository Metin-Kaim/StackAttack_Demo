using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.Signals
{
    public class PoolSignals : MonoBehaviour
    {
        public static PoolSignals Instance;

        public Func<ItemType, GameObject> onGetItemFromPool;
        public UnityAction<ItemType, GameObject> onItemReleased;

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
    }
}