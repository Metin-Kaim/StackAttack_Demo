using Assets.Game.Scripts.Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private Transform bulletPoint;
        [SerializeField] private float fireRate = 1.0f;

        float _timer;

        private void Start()
        {
            _timer = fireRate;
        }

        private void Update()
        {
            if (InputSignals.Instance.onGetIsTouching.Invoke())
            {
                _timer += Time.deltaTime;

                if (_timer >= fireRate)
                {
                    _timer = 0;
                    GameObject bullet = PoolSignals.Instance.onGetItemFromPool?.Invoke(ItemTypes.Bullet);
                    bullet.transform.position = bulletPoint.position;
                }
            }
        }
    }
}