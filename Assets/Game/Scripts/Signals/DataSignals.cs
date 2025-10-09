using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Signals
{
    public class DataSignals : MonoBehaviour
    {
        public static DataSignals Instance;

        public Func<ColorTypes, Color> onGetColor;

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