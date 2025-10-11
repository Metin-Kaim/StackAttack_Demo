using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

public class RocketHandler : AbsAmmunitionHandler
{
    private Vector3 _lastPos;
    private float _phaseOffset;

    protected override void Move()
    {
        base.Move();

        float t = timer / data.duration;
        float waveOffset = Mathf.Sin(t * (data as RocketData).frequency * Mathf.PI * 2 + _phaseOffset) * (data as RocketData).amplitude;

        transform.position += Time.deltaTime * waveOffset * Vector3.right;

        Vector3 direction = (transform.position - _lastPos).normalized;
        if (direction.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, (data as RocketData).rotationSmoothness * Time.deltaTime);
        }

        _lastPos = transform.position;
    }

    public override void Launch()
    {
        _phaseOffset = Random.Range(-3, 3);
        _lastPos = transform.position;
        base.Launch();
    }

    protected override void Hit(StackHolderHandler stack)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, (data as RocketData).ExplosionRadius, data.targetMask);

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out StackHolderHandler handler))
                {
                    handler.Hit(data.damage);
                }
            }
        }

        PoolSignals.Instance.onItemReleased?.Invoke(itemType, gameObject);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, (data as RocketData).ExplosionRadius);
    }
}
