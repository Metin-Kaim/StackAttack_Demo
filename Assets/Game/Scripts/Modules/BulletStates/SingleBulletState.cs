using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using UnityEngine;

namespace Assets.Game.Scripts.Modules.BulletStates
{
    public class SingleBulletState : AbsBulletState
    {
        public SingleBulletState(Transform centerBuletPoint, AbsAmmunitionModule module) : base(centerBuletPoint, module)
        { }

        public override void Tick()
        {
            FireBullet(centerBulletPoint.position);
        }
    }
}