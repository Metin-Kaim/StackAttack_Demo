using Assets.Game.Scripts.Controllers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputController inputController;

        private void OnEnable()
        {
            InputSignals.Instance.onGetMouseDeltaX += OnGetMouseDelta;
        }

        private float OnGetMouseDelta()
        {
            return inputController.MouseDelta.x;
        }

        private void OnDisable()
        {
            InputSignals.Instance.onGetMouseDeltaX -= OnGetMouseDelta;
        }
    }
}