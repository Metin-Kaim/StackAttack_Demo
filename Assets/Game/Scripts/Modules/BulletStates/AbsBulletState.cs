
using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Modules.BulletStates
{
    public abstract class AbsBulletState
    {
        protected Transform centerBulletPoint;
        private AbsAmmunitionModule module;

        public BulletData BulletData;

        protected AbsBulletState(Transform centerBulletPoint, AbsAmmunitionModule module)
        {
            BulletData = (module as BulletModule).BulletData;

            this.centerBulletPoint = centerBulletPoint;
            this.module = module;
        }

        protected void FireBullet(Vector3 bulletPosition)
        {
            GameObject bullet = PoolSignals.Instance.onGetItemFromPool?.Invoke(ItemType.Bullet);
            bullet.transform.position = bulletPosition;
            AbsAmmunitionHandler ammunition = bullet.GetComponent<AbsAmmunitionHandler>();
            ammunition.Initialize(BulletData);
            ammunition.Launch();
        }

        public abstract void Tick();
    }
}