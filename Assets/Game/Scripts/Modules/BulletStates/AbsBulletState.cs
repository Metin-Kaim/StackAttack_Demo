
using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Modules.BulletStates
{
    public abstract class AbsBulletState
    {
        protected Transform centerBulletPoint;
        private AbsModuleData moduleData;

        protected AbsBulletState(Transform centerBulletPoint, AbsModuleData moduleData)
        {
            this.centerBulletPoint = centerBulletPoint;
            this.moduleData = moduleData;
        }

        protected void FireBullet(Vector3 bulletPosition)
        {
            GameObject bullet = PoolSignals.Instance.onGetItemFromPool?.Invoke(ItemType.Bullet);
            bullet.transform.position = bulletPosition;
            AbsAmmunition ammunition = bullet.GetComponent<AbsAmmunition>();
            ammunition.Initialize(moduleData);
            ammunition.Launch();
        }

        public abstract void Tick();
    }
}