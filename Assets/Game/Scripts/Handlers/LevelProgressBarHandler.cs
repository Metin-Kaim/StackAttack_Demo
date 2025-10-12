using Assets.Game.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Handlers
{
    public class LevelProgressBarHandler : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
        [SerializeField] private float levelHeight;

        public void Update()
        {
            float playerY = PlayerSignals.Instance.onGetPlayerPositionY.Invoke();
            progressBar.fillAmount = playerY / levelHeight;

            if(progressBar.fillAmount >= 1f)
            {
                Debug.Log("Level Complete!");
                GameSignals.Instance.onLevelCompleted?.Invoke();
            }
        }
    }
}