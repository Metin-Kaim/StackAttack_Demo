using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

public class RocketHandler : AbsAmmunition
{
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequency = 3f;
    [SerializeField] private float rotationSmoothness = 10f;
    [SerializeField] private float explosionRadius;

    private Vector3 _lastPos;
    private float _phaseOffset; // 🔸 Dalga yönünü belirleyecek

    protected override void Move()
    {
        // Base hareket: lineer yukarı hareket ve timer kontrolü
        base.Move();

        // Dalgalı hareket (x ekseninde)
        float t = timer / duration; // normalize edilmiş zaman
        float waveOffset = Mathf.Sin(t * frequency * Mathf.PI * 2 + _phaseOffset) * amplitude;

        transform.position += Time.deltaTime * waveOffset * Vector3.right;

        // Rotasyon
        Vector3 direction = (transform.position - _lastPos).normalized;
        if (direction.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotationSmoothness * Time.deltaTime);
        }

        _lastPos = transform.position;
    }

    public override void Launch()
    {
        base.Launch();

        _phaseOffset = Random.Range(-3, 3);
        _lastPos = transform.position;
    }

    protected override void Hit(StackHolderHandler stack)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, (module as RocketModuleData).ExplosionRadius, targetMask);

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out StackHolderHandler handler))
                {
                    handler.Hit(damage);
                }
            }
        }

        PoolSignals.Instance.onItemReleased?.Invoke(itemType, gameObject);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
