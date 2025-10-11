using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.Signals
{
    public class CanvasSignals : MonoBehaviour
    {
        public static CanvasSignals Instance;

        public UnityAction onShowUpgradeCards;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}