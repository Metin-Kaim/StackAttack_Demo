using Assets.Game.Scripts.Datas;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.Signals
{
    public class UpgradeSignals : MonoBehaviour
    {
        public static UpgradeSignals Instance;

        public UnityAction<UpgradeData> onUpgradeApplied;

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