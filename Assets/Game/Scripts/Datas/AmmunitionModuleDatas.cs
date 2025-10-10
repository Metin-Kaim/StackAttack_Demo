using System;
    
namespace Assets.Game.Scripts.Datas
{
    [Serializable]
    public class AmmunitionModuleDatas
    {
        public BulletModuleData BulletModuleData;
        public RocketModuleData RocketModuleInfo;
    }

    [Serializable]
    public class BulletModuleData : AbsModuleData
    {
        public bool Piercing;
    }

    [Serializable]
    public class RocketModuleData : AbsModuleData
    {
        public float ExplosionRadius;
        public float LaunchDelay;
    }

    [Serializable]
    public abstract class AbsModuleData
    {
        public ModuleType ModuleType;
        public float FireRate;
        public byte AmmoCount;
    }
}