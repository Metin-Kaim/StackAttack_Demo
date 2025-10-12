using Assets.Game.Scripts.Managers;
using Assets.Game.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Handlers
{
    public class LevelProgressBarHandler : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        private Transform _levelEndPoint;

        private void Start()
        {
            _levelEndPoint = LevelSignals.Instance.onGetCurrentLevel.Invoke().LevelEndPoint;
        }

        public void Update()
        {
            float playerY = PlayerSignals.Instance.onGetPlayerPositionY.Invoke();
            progressBar.fillAmount = playerY / _levelEndPoint.position.y;

            if (progressBar.fillAmount >= 1f)
            {
                Debug.Log("Level Complete!");
                GameManager.Instance.onLevelCompleted?.Invoke();
            }
        }
    }
}