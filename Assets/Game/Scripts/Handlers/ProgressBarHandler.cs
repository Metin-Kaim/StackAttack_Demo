using Assets.Game.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Handlers
{
    public class ProgressBarHandler : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        [SerializeField] private float fillSpeed = 0.05f;

        public void IncreaseProgress()
        {
            progressBar.fillAmount += fillSpeed;

            if (progressBar.fillAmount >= 1)
            {
                progressBar.fillAmount = 0;
                CanvasSignals.Instance.onShowUpgradeCards?.Invoke();
            }
        }
    }
}