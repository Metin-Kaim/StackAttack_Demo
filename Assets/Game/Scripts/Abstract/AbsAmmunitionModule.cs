using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Abstract
{
    public abstract class AbsAmmunitionModule
    {
        protected Transform bulletPoint;
        protected float fireTimer;
        protected byte ammoCount;
        protected float fireRate;

        protected AbsAmmunitionModule(AbsModuleData data)
        {
            this.fireRate = data.FireRate;
            this.ammoCount = data.AmmoCount;
        }

        public abstract ModuleType ModuleType { get; }

        public virtual void Initialize(Transform bulletPoint)
        {
            UpgradeSignals.Instance.onUpgradeApplied += OnUpgradeReceived;
            this.bulletPoint = bulletPoint;
        }

        private void OnUpgradeReceived(UpgradeData data)
        {
            if (data.TargetModuleType == ModuleType)
            {
                ApplyUpgrade(data);
            }
        }

        public abstract void Tick();
        protected abstract void Fire();
        protected abstract void ApplyUpgrade(UpgradeData data);

        public virtual void Reset()
        {
            fireTimer = 0;
        }

        //public virtual void Dispose()
        //{
        //    UpgradeSignals.Instance.onUpgradeApplied -= OnUpgradeReceived;
        //}
    }
}