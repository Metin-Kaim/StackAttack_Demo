using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Modules
{
    public class RocketModule : AbsAmmunitionModule
    {
        private float explosionRadius;
        private int rocketsFired = 0;
        private bool isFiring = false;
        private float rocketDelayTimer = 0f;
        private float launchDelay;

        public RocketModule(AbsModuleData info) : base(info)
        {
            explosionRadius = (info as RocketModuleData).ExplosionRadius;
            launchDelay = (info as RocketModuleData).LaunchDelay;
        }

        public override ModuleType ModuleType => ModuleType.Rocket;

        public override void Tick()
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= 1f / fireRate)
            {
                fireTimer = 0;
                Fire();
            }

            // Delayli spawn kontrolü
            if (isFiring)
            {
                rocketDelayTimer += Time.deltaTime;
                if (rocketDelayTimer >= launchDelay)
                {
                    rocketDelayTimer = 0f;
                    SpawnRocket();
                    rocketsFired++;

                    if (rocketsFired >= ammoCount)
                        isFiring = false;
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
            rocket.GetComponent<AbsAmmunition>().Launch();
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
                case UpgradeType.ExplosionRadius:
                    explosionRadius += data.Value;
                    break;
            }
        }
    }
}
