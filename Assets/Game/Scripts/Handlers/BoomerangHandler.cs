using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

public class BoomerangHandler : AbsAmmunition
{
    [Header("Boomerang Settings")]
    [SerializeField] private float arcHeight = 2f;
    [SerializeField] private float rotationSpeed = 720f;

    private Vector3 _direction;
    private bool _isReturning;

    protected override void Move()
    {
        timer += Time.deltaTime;

        float t = timer / duration;

        Vector3 moveDir = _isReturning ? -_direction : _direction;

        float heightOffset = Mathf.Sin(t * Mathf.PI) * arcHeight;

        transform.position += speed * Time.deltaTime * moveDir;
        transform.position += heightOffset * Time.deltaTime * Vector3.up;

        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        if (timer >= duration)
        {
            if (!_isReturning)
            {
                _isReturning = true;
                timer = 0f;
                speed *= 1.2f;
            }
            else
            {
                PoolSignals.Instance.onItemReleased?.Invoke(itemType, gameObject);
                return;
            }
        }
    }

    public override void Launch()
    {
        base.Launch();

        _isReturning = false;

        float randomAngle = Random.Range(-30f, 30f);
        _direction = Quaternion.Euler(0, 0, randomAngle) * Vector3.up;
    }

    protected override void Hit(StackHolderHandler stack)
    {
        stack.Hit(damage);
    }
}
