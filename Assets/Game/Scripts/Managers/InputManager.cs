using Assets.Game.Scripts.Controllers;
using Assets.Game.Scripts.Signals;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputController inputController;

        private void OnEnable()
        {
            InputSignals.Instance.onGetMouseDeltaX += OnGetMouseDelta;
            InputSignals.Instance.onGetIsTouching += OnGetIsTouching;
        }

        private bool OnGetIsTouching()
        {
            return inputController.IsTouching;
        }

        private float OnGetMouseDelta()
        {
            return inputController.MouseDelta.x;
        }

        private void OnDisable()
        {
            InputSignals.Instance.onGetMouseDeltaX -= OnGetMouseDelta;
            InputSignals.Instance.onGetIsTouching -= OnGetIsTouching;
        }
    }
}