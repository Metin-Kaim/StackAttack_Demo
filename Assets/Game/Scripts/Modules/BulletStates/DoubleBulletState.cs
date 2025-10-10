using Assets.Game.Scripts.Datas;
using UnityEngine;

namespace Assets.Game.Scripts.Modules.BulletStates
{
    public class DoubleBulletState : AbsBulletState
    {
        Vector3 rightPoint;
        Vector3 leftPoint;

        Vector3 _bulletPositionOffset = Vector3.right * 0.25f;

        public DoubleBulletState(Transform centerBulletPoint, AbsModuleData moduleData) : base(centerBulletPoint, moduleData)
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