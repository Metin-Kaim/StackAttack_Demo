using Assets.Game.Scripts.Managers;
using Assets.Game.Scripts.Signals;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Handlers
{
    public class LevelProgressBarHandler : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        private Transform _levelEndPoint;
        private bool _isLevelCompleted;

        private void Start()
        {
            _levelEndPoint = LevelSignals.Instance.onGetCurrentLevel.Invoke().LevelEndPoint;
        }

        public void Update()
        {
            if(_isLevelCompleted || _levelEndPoint == null) return;

            float playerY = PlayerSignals.Instance.onGetPlayerPositionY.Invoke();
            progressBar.fillAmount = playerY / _levelEndPoint.position.y;

            if (progressBar.fillAmount >= 1f)
            {
                _isLevelCompleted = true;
                GameManager.Instance.onLevelCompleted?.Invoke();
            }
        }
    }
}