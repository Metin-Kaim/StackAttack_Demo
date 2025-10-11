using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Abstract
{
    public abstract class AbsAmmunitionHandler : MonoBehaviour
    {
        [SerializeField] protected ItemType itemType;
        

        protected HashSet<StackHolderHandler> hitTargets = new();

        protected float timer;
        protected TrailRenderer trailRenderer;
        protected bool isLaunched;
        protected AbsAmmunitionData data;

        private void Awake()
        {
            trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        private void OnDisable()
        {
            trailRenderer.Clear();
        }

        protected virtual void Update()
        {
            if (!isLaunched) return;

            Move();

            CheckHits();
        }

        protected void CheckHits()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, data.radius, data.targetMask);

            if (hit == null) return;

            if (hit.TryGetComponent(out StackHolderHandler stack))
            {
                if (hitTargets.Add(stack))
                {
                    Hit(stack);
                }
            }
        }
        public virtual void Initialize(AbsAmmunitionData data)
        {
            this.data = data;

        }
        protected virtual void Hit(StackHolderHandler stack)
        {
            stack.Hit(data.damage);
            PoolSignals.Instance.onItemReleased?.Invoke(itemType, gameObject);
        }

        protected virtual void Move()
        {
            transform.position += data.speed * Time.deltaTime * Vector3.up;

            timer += Time.deltaTime;
            if (timer >= data.duration)
            {
                PoolSignals.Instance.onItemReleased?.Invoke(itemType, gameObject);
                return;
            }
        }

        public virtual void Launch()
        {
            timer = 0;
            isLaunched = true;
            hitTargets.Clear();
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, data.radius);
        }
#endif
    }
}