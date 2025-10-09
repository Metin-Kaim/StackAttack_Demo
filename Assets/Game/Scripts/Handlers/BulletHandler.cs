using Assets.Game.Scripts.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Handlers
{
    public class BulletHandler : MonoBehaviour
    {
        [SerializeField] ItemTypes itemType;
        [SerializeField] float duration = 2f;
        [SerializeField] float speed = 5f;
        [SerializeField] float radius = 0.2f;
        [SerializeField] LayerMask targetMask;

        private float _timer;
        private readonly HashSet<StackHolderHandler> _hitTargets = new();

        private void OnEnable()
        {
            _timer = 0;
            _hitTargets.Clear();
        }

        private void Update()
        {
            transform.position += speed * Time.deltaTime * Vector3.up;

            _timer += Time.deltaTime;
            if (_timer >= duration)
            {
                PoolSignals.Instance.onItemReleased?.Invoke(itemType, gameObject);
                return;
            }

            CheckHits();
        }

        private void CheckHits()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, targetMask);

            if (hit == null)
                return;

            if (hit.TryGetComponent(out StackHolderHandler stack))
            {
                if (_hitTargets.Add(stack))
                {
                    stack.Hit(1);
                    PoolSignals.Instance.onItemReleased?.Invoke(itemType, gameObject);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}
