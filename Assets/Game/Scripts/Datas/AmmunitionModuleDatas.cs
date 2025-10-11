using System;
using UnityEngine;

namespace Assets.Game.Scripts.Datas
{
    [Serializable]
    public class AmmunitionModuleDatas
    {
        public BulletModuleData BulletModuleData;
        [Space]
        public RocketModuleData RocketModuleData;
        [Space]
        public BoomerangModuleData BoomerangModuleData;
    }

    [Serializable]
    public class BulletModuleData : AbsModuleData
    {
        public BulletData BulletData;
    }

    [Serializable]
    public class RocketModuleData : AbsModuleData
    {
        public RocketData RocketData;
    }

    [Serializable]
    public class BoomerangModuleData : AbsModuleData
    {
        public BoomerangData BoomerangData;
    }

    [Serializable]
    public abstract class AbsModuleData
    {
        public float FireRate;
        public byte AmmoCount;
    }
}