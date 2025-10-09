
using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Modules.BulletStates
{
    public abstract class AbsBulletState
    {
        protected Transform centerBulletPoint;

        protected AbsBulletState(Transform centerBulletPoint)
        {
            this.centerBulletPoint = centerBulletPoint;
        }

        protected void FireBullet(Vector3 bulletPosition)
        {
            GameObject bullet = PoolSignals.Instance.onGetItemFromPool?.Invoke(ItemType.Bullet);
            bullet.transform.position = bulletPosition;
            bullet.GetComponent<AbsAmmunition>().Launch();
        }

        public abstract void Tick();
    }
}