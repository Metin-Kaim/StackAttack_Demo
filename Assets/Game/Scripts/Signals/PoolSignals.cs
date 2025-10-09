using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.Signals
{
    public class PoolSignals : MonoBehaviour
    {
        public static PoolSignals Instance;

        public Func<ItemTypes, GameObject> onGetItemFromPool;
        public UnityAction<ItemTypes, GameObject> onItemReleased;

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