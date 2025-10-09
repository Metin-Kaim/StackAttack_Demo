using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Modules.BulletStates;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Game.Scripts.Modules
{
    public class BulletModule : AbsAmmunitionModule
    {
        private bool piercing = false;

        public override ModuleType ModuleType => ModuleType.Bullet;

        private AbsBulletState currentBulletState;
        SingleBulletState singleBulletState;
        DoubleBulletState doubleBulletState;

        Dictionary<byte, AbsBulletState> bulletStates;

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
            currentBulletState?.Tick();
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
                    UpgradeBulletState(ammoCount);
                    break;
                case UpgradeType.Piercing:
                    piercing = true;
                    break;
            }
        }

        public override void Initialize(Transform bulletPoint)
        {
            base.Initialize(bulletPoint);

            singleBulletState = new SingleBulletState(bulletPoint);
            doubleBulletState = new DoubleBulletState(bulletPoint);

            bulletStates = new()
            {
                {1, singleBulletState},
                {2, doubleBulletState},
            };

            currentBulletState = singleBulletState;
        }

        public void UpgradeBulletState(byte bulletCount)
        {
            AbsBulletState selectedState = bulletStates.FirstOrDefault(x => x.Key == bulletCount).Value;
            if (selectedState == null)
            {
                Debug.Log("Failed to switch next bullet state");
                return;
            }
            currentBulletState = selectedState;
        }
    }
}