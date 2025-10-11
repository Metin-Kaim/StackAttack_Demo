using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Modules
{
    public class RocketModule : AbsAmmunitionModule
    {
        private int rocketsFired;
        private bool isFiring;
        private float rocketDelayTimer;

        private RocketModuleData rocketModuleData;
        private RocketData rocketData;

        public RocketModule(AbsModuleData moduleData) : base(moduleData)
        {
            rocketModuleData = (moduleData as RocketModuleData);
            rocketData = rocketModuleData.RocketData;
        }

        public override ModuleType ModuleType => ModuleType.Rocket;

        public override void Tick()
        {
            if (isFiring)
            {
                rocketDelayTimer += Time.deltaTime;
                if (rocketDelayTimer >= rocketData.LaunchDelay)
                {
                    rocketDelayTimer = 0f;
                    SpawnRocket();
                    rocketsFired++;

                    if (rocketsFired >= rocketModuleData.AmmoCount)
                        isFiring = false;
                }
            }
            else
            {
                fireTimer += Time.deltaTime;

                if (fireTimer >= 1f / rocketModuleData.FireRate)
                {
                    fireTimer = -((rocketData.duration * rocketModuleData.AmmoCount) + (rocketData.LaunchDelay * rocketModuleData.AmmoCount));
                    Fire();
                }
            }
        }

        protected override void Fire()
        {
            isFiring = true;
            rocketsFired = 0;
            rocketDelayTimer = 0f;
        }

        private void SpawnRocket()
        {
            GameObject rocket = PoolSignals.Instance.onGetItemFromPool?.Invoke(ItemType.Rocket);
            rocket.transform.position = bulletPoint.position;
            AbsAmmunitionHandler ammunition = rocket.GetComponent<AbsAmmunitionHandler>();
            ammunition.Initialize(rocketData);
            ammunition.Launch();
        }

        protected override void ApplyUpgrade(UpgradeData data)
        {
            switch (data.UpgradeType)
            {
                case UpgradeType.FireRate:
                    rocketModuleData.FireRate += rocketModuleData.FireRate * data.Multiplier;
                    break;
                case UpgradeType.ExtraAmmo:
                    rocketModuleData.AmmoCount += (byte)Mathf.RoundToInt(data.Value);
                    break;
                case UpgradeType.ExplosionRadius:
                    rocketData.ExplosionRadius += data.Value;
                    break;
            }
        }
    }
}
