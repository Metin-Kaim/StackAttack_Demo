using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Modules
{
    public class BulletModule : AbsAmmunitionModule
    {
        private bool piercing = false;

        public override ModuleType ModuleType => ModuleType.Bullet;

        public override void Tick()
        {
            Debug.Log("Firing");

            fireTimer += Time.deltaTime;

            if (fireTimer >= 1f / fireRate)
            {
                fireTimer = 0;
                Fire();
            }
        }

        protected override void Fire()
        {
            GameObject bullet = PoolSignals.Instance.onGetItemFromPool?.Invoke(ItemType.Bullet);
            bullet.transform.position = bulletPoint.position;
            bullet.GetComponent<AbsAmmunition>().Launch();
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
                case UpgradeType.Piercing:
                    piercing = true;
                    break;
            }
        }
    }
}