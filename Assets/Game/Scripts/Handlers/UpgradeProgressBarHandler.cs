using Assets.Game.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Handlers
{
    public class UpgradeProgressBarHandler : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        [SerializeField] private float fillSpeed;

        public void IncreaseProgress()
        {
            progressBar.fillAmount += fillSpeed;

            if (progressBar.fillAmount >= 1)
            {
                progressBar.fillAmount = 0;
                fillSpeed -= 0.05f;
                if (fillSpeed < 0.1f)
                    fillSpeed = 0.1f;

                CanvasSignals.Instance.onShowUpgradeCards?.Invoke();
            }
        }
    }
}