using Assets.Game.Scripts.Signals;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerSignals.Instance.onGetPlayerPositionY += OnGetPlayerPositionY;
        }

        private float OnGetPlayerPositionY()
        {
            return transform.position.y;
        }

        private void OnDisable()
        {
            PlayerSignals.Instance.onGetPlayerPositionY -= OnGetPlayerPositionY;
        }
    }
}