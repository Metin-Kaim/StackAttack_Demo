using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

public class BoomerangHandler : AbsAmmunitionHandler
{
    private Vector3 _direction;
    private bool _isReturning;

    protected override void Move()
    {
        timer += Time.deltaTime;

        float t = timer / data.duration;

        Vector3 moveDir;

        if (_isReturning)
        {
            // Player pozisyonunu al
            Vector3 playerPos = PlayerSignals.Instance.onGetPlayerPosition.Invoke() + Vector2.up;

            // Player'a doğru yön vektörü
            moveDir = (playerPos - transform.position).normalized;

            // Eğer çok yaklaştıysa boomerang'ı pool'a geri gönder
            if (Vector3.Distance(transform.position, playerPos) < 1f)
            {
                PoolSignals.Instance.onItemReleased?.Invoke(itemType, gameObject);
                return;
            }
        }
        else
        {
            moveDir = _direction;
        }

        float heightOffset = Mathf.Sin(t * Mathf.PI) * (data as BoomerangData).arcHeight;

        transform.position += data.speed * Time.deltaTime * moveDir;
        transform.position += heightOffset * Time.deltaTime * Vector3.up;

        transform.Rotate(Vector3.forward, (data as BoomerangData).rotationSpeed * Time.deltaTime);

        if (timer >= data.duration && !_isReturning)
        {
            _isReturning = true;
            timer = 0f;
            hitTargets.Clear();
        }
    }


    public override void Launch()
    {
        _isReturning = false;
        float randomAngle = Random.Range(-30f, 30f);
        _direction = Quaternion.Euler(0, 0, randomAngle) * Vector3.up;

        base.Launch();
    }

    protected override void Hit(StackHolderHandler stack)
    {
        stack.Hit(data.damage);
    }
}
