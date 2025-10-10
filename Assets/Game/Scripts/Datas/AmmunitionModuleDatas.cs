using System;
    
namespace Assets.Game.Scripts.Datas
{
    [Serializable]
    public class AmmunitionModuleDatas
    {
        public BulletModuleData BulletModuleData;
        public RocketModuleData RocketModuleData;
        public BoomerangModuleData BoomerangModuleData;
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
    public class BoomerangModuleData : AbsModuleData
    {
        //public float ReturnDelay;
        //public float MaxDistance;
        //public float RotationSpeed;
        public float Size;
    }

    [Serializable]
    public abstract class AbsModuleData
    {
        public ModuleType ModuleType;
        public float FireRate;
        public byte AmmoCount;
    }
}