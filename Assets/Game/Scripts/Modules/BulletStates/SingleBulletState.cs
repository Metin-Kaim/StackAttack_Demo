using UnityEngine;

namespace Assets.Game.Scripts.Modules.BulletStates
{
    public class SingleBulletState : AbsBulletState
    {
        public SingleBulletState(Transform centerBuletPoint) : base(centerBuletPoint)
        { }

        public override void Tick()
        {
            FireBullet(centerBulletPoint.position);
        }
    }
}