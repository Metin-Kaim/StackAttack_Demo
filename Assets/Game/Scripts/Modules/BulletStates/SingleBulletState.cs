using Assets.Game.Scripts.Datas;
using UnityEngine;

namespace Assets.Game.Scripts.Modules.BulletStates
{
    public class SingleBulletState : AbsBulletState
    {
        public SingleBulletState(Transform centerBuletPoint, AbsModuleData moduleData) : base(centerBuletPoint, moduleData)
        { }

        public override void Tick()
        {
            FireBullet(centerBulletPoint.position);
        }
    }
}