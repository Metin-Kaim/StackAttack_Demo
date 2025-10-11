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
        private AbsBulletState currentBulletState;

        SingleBulletState singleBulletState;
        DoubleBulletState doubleBulletState;

        Dictionary<byte, AbsBulletState> bulletStates;

        private BulletModuleData bulletModuleData;
        private BulletData bulletData;

        public override ModuleType ModuleType => ModuleType.Bullet;

        public BulletData BulletData => bulletData; 

        public BulletModule(AbsModuleData moduleData) : base(moduleData)
        {
            bulletModuleData = (moduleData as BulletModuleData);
            bulletData = (moduleData as BulletModuleData).BulletData;
        }

        public override void Tick()
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= 1f / bulletModuleData.FireRate)
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
                    bulletModuleData.FireRate += bulletModuleData.FireRate * data.Multiplier;
                    break;
                case UpgradeType.ExtraAmmo:
                    bulletModuleData.AmmoCount += (byte)Mathf.RoundToInt(data.Value);
                    UpgradeBulletState(bulletModuleData.AmmoCount);
                    break;
                case UpgradeType.Piercing:
                    bulletData.Piercing = true;
                    break;
            }
        }

        public override void Initialize(Transform bulletPoint)
        {
            base.Initialize(bulletPoint);

            singleBulletState = new SingleBulletState(bulletPoint, this);
            doubleBulletState = new DoubleBulletState(bulletPoint, this);

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