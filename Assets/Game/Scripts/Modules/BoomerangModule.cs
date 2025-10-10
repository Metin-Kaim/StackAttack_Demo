using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Modules
{
    public class BoomerangModule : AbsAmmunitionModule
    {
        private float size;
        private bool isFiring;
        private float boomerangDelayTimer;
        private float launchDelay;
        private int boomerangsFired;

        public BoomerangModule(AbsModuleData data) : base(data)
        {
            base.moduleData = moduleData;
            size = (data as BoomerangModuleData).Size;
        }

        public override ModuleType ModuleType => ModuleType.Boomerang;

        public override void Tick()
        {
            if (isFiring)
            {
                boomerangDelayTimer += Time.deltaTime;
                if (boomerangDelayTimer >= launchDelay)
                {
                    boomerangDelayTimer = 0f;
                    SpawnBoomerang();
                    boomerangsFired++;

                    if (boomerangsFired >= ammoCount)
                        isFiring = false;
                }
            }
            else
            {
                fireTimer += Time.deltaTime;

                if (fireTimer >= 1f / fireRate)
                {
                    fireTimer = 0;
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
            AbsAmmunition ammunition = boomerang.GetComponent<AbsAmmunition>();
            ammunition.Initialize(moduleData);
            ammunition.Launch();
        }
        protected override void ApplyUpgrade(UpgradeData data)
        {
            switch (data.UpgradeType)
            {
                case UpgradeType.FireRate:
                    fireRate *= data.Multiplier;
                    break;
                case UpgradeType.ExtraAmmo:
                    ammoCount += (byte)Mathf.RoundToInt(data.Value);
                    break;
                case UpgradeType.Size:
                    size *= data.Multiplier;
                    break;
            }
        }
    }
}