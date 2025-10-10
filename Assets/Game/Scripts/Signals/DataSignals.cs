using Assets.Game.Scripts.Datas;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Signals
{
    public class DataSignals : MonoBehaviour
    {
        public static DataSignals Instance;

        public Func<ColorType, Color> onGetColor;
        public Func<ModuleType, AbsModuleData> onGetModuleData;

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