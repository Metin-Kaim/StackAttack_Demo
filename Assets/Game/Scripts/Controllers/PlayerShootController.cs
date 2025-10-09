using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Signals;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject rocket = PoolSignals.Instance.onGetItemFromPool?.Invoke(ItemTypes.Rocket);
                rocket.transform.position = bulletPoint.position;
                rocket.GetComponent<AbsAmmunition>().Launch();
            }

            if (InputSignals.Instance.onGetIsTouching.Invoke())
            {
                _timer += Time.deltaTime;

                if (_timer >= fireRate)
                {
                    _timer = 0;
                    GameObject bullet = PoolSignals.Instance.onGetItemFromPool?.Invoke(ItemTypes.Bullet);
                    bullet.transform.position = bulletPoint.position;
                    bullet.GetComponent<AbsAmmunition>().Launch();
                }
            }
        }
    }
}