using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Datas
{
    [System.Serializable]
    public class UpgradeData
    {
        public ModuleType TargetModuleType;
        public UpgradeType UpgradeType;
        public float Value;
        public float Multiplier;

        public UpgradeData(ModuleType target, UpgradeType type, float value = 0f, float multiplier = 1f)
        {
            TargetModuleType = target;
            UpgradeType = type;
            Value = value;
            Multiplier = multiplier;
        }
    }
}