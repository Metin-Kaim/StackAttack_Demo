using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class InputController : MonoBehaviour
    {
        private Vector2 _lastMousePosition;
        private bool _isTouching;

        public Vector2 MouseDelta { get; private set; }
        public bool IsTouching => _isTouching;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                _lastMousePosition = Input.mousePosition;
                MouseDelta = Vector2.zero;
            }
            else if (Input.GetMouseButton(0) && _isTouching)
            {
                Vector2 currentMousePosition = Input.mousePosition;
                MouseDelta = currentMousePosition - _lastMousePosition;
                _lastMousePosition = currentMousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                MouseDelta = Vector2.zero;
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerSignals.Instance.onAddModule?.Invoke(ModuleType.Rocket);
            }
            if (Input.GetKeyUp(KeyCode.T))
            {
                UpgradeSignals.Instance.onUpgradeApplied?.Invoke(new UpgradeData(
                    ModuleType.Bullet,
                    UpgradeType.FireRate,
                    multiplier: 1.2f
                ));
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                UpgradeSignals.Instance.onUpgradeApplied?.Invoke(new UpgradeData(ModuleType.Bullet, UpgradeType.ExtraAmmo, value: 1));
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                PlayerSignals.Instance.onAddModule?.Invoke(ModuleType.Boomerang);
            }
        }
    }
}