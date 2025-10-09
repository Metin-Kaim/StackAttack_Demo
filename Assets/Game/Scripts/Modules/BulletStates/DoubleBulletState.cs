using UnityEngine;

namespace Assets.Game.Scripts.Modules.BulletStates
{
    public class DoubleBulletState : AbsBulletState
    {
        Vector3 rightPoint;
        Vector3 leftPoint;

        Vector3 _bulletPositionOffset = Vector3.right * 0.2f;

        public DoubleBulletState(Transform centerBulletPoint) : base(centerBulletPoint)
        { }

        public override void Tick()
        {
            rightPoint = centerBulletPoint.position + _bulletPositionOffset;
            leftPoint = centerBulletPoint.position - _bulletPositionOffset;

            FireBullet(rightPoint);
            FireBullet(leftPoint);
        }
    }
}