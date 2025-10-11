using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Assets.Game.Scripts.Modules
{
    public class BoomerangModule : AbsAmmunitionModule
    {
        private bool isFiring;
        private float boomerangDelayTimer;
        private int boomerangsFired;

        private BoomerangData boomerangData;
        private BoomerangModuleData boomerangModuleData;

        public BoomerangModule(AbsModuleData moduleData) : base(moduleData)
        {
            boomerangModuleData = moduleData as BoomerangModuleData;
            boomerangData = boomerangModuleData.BoomerangData;
        }

        public override ModuleType ModuleType => ModuleType.Boomerang;

        public override void Tick()
        {
            if (isFiring)
            {
                boomerangDelayTimer += Time.deltaTime;
                if (boomerangDelayTimer >= boomerangData.LaunchDelay)
                {
                    boomerangDelayTimer = 0f;
                    SpawnBoomerang();
                    boomerangsFired++;

                    if (boomerangsFired >= boomerangModuleData.AmmoCount)
                        isFiring = false;
                }
            }
            else
            {
                fireTimer += Time.deltaTime;

                if (fireTimer >= 1f / boomerangModuleData.FireRate)
                {
                    fireTimer = -((boomerangData.duration * boomerangModuleData.AmmoCount) + (boomerangData.LaunchDelay * boomerangModuleData.AmmoCount));
                    Fire();
                }
            }
        }
        protected override void Fire()
        {
            isFiring = true;
            boomerangsFired = 0;
            boomerangDelayTimer = 0f;
        }
        private void SpawnBoomerang()
        {
            GameObject boomerang = PoolSignals.Instance.onGetItemFromPool?.Invoke(ItemType.Boomerang);
            boomerang.transform.position = bulletPoint.position;
            AbsAmmunitionHandler ammunition = boomerang.GetComponent<AbsAmmunitionHandler>();
            ammunition.Initialize(boomerangData);
            ammunition.Launch();
        }
        protected override void ApplyUpgrade(UpgradeData data)
        {
            switch (data.UpgradeType)
            {
                case UpgradeType.FireRate:
                    boomerangModuleData.FireRate += boomerangModuleData.FireRate * data.Multiplier;
                    break;
                case UpgradeType.ExtraAmmo:
                    boomerangModuleData.AmmoCount += (byte)Mathf.RoundToInt(data.Value);
                    break;
                case UpgradeType.Size:
                    boomerangData.radius += boomerangData.radius * data.Multiplier;
                    break;
            }
        }
    }
}