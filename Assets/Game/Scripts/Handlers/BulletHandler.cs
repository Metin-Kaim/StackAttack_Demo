using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;

namespace Assets.Game.Scripts.Handlers
{
    public class BulletHandler : AbsAmmunitionHandler
    {
        private byte _hitCount;

        protected override void Hit(StackHolderHandler stack)
        {
            stack.Hit(data.damage);
            if (_hitCount == 0 && (data as BulletData).Piercing)
            {
                _hitCount++;
                return;
            }

            PoolSignals.Instance.onItemReleased?.Invoke(itemType, gameObject);
        }

        public override void Launch()
        {
            _hitCount = 0;
            base.Launch();
        }
    }
}
