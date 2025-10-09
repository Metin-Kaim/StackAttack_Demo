using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Modules
{
    public class RocketModule : AbsAmmunitionModule
    {
        public override ModuleType ModuleType => ModuleType.Rocket;

        private float explosionRadius = 1f;

        public override void Tick()
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= 1f / fireRate)
            {
                fireTimer = 0;
                Fire();
            }
        }

        protected override void Fire()
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
                    ammoCount += Mathf.RoundToInt(data.Value);
                    break;
                case UpgradeType.ExplosionRadius:
                    explosionRadius += data.Value;
                    break;
            }
        }
    }
}
