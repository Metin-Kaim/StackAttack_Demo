using Assets.Game.Scripts.Abstract;
using Assets.Game.Scripts.Modules;
using Assets.Game.Scripts.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private Transform bulletPoint;

        private bool _isTouchReleased;

        private readonly List<AbsAmmunitionModule> _activeModules = new();
        private Dictionary<ModuleType, AbsAmmunitionModule> allModules;

        private void OnEnable()
        {
            PlayerSignals.Instance.onAddModule += AddModule;
        }
        private void OnDisable()
        {
            PlayerSignals.Instance.onAddModule -= AddModule;
        }

        private void Start()
        {
            BulletModule bulletModule = new(DataSignals.Instance.onGetModuleData.Invoke(ModuleType.Bullet));
            RocketModule rocketModule = new(DataSignals.Instance.onGetModuleData.Invoke(ModuleType.Rocket));

            allModules = new()
            {
                {ModuleType.Bullet, bulletModule},
                {ModuleType.Rocket, rocketModule},
            };

            AddModule(ModuleType.Bullet);
        }

        private void Update()
        {
            if (InputSignals.Instance.onGetIsTouching.Invoke())
            {
                foreach (var module in _activeModules)
                {
                    module.Tick();
                }

                _isTouchReleased = false;
            }
            else if (!_isTouchReleased)
            {
                _isTouchReleased = true;

                foreach (var module in _activeModules)
                {
                    module.Reset();
                }
            }
        }

        public void AddModule(ModuleType type)
        {
            allModules.TryGetValue(type, out AbsAmmunitionModule module);

            if (module == null)
            {
                Debug.LogWarning("No module of this type was found!");
                return;
            }

            if (_activeModules.Contains(module))
            {
                Debug.LogWarning("This module has already been added!");
                return;
            }

            _activeModules.Add(module);
            module.Initialize(bulletPoint);
        }

        //public void RemoveModule(AbsAmmunitionModule module)
        //{
        //    module.Dispose();
        //    _activeModules.Remove(module);
        //}
    }
}