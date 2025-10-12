using Assets.Game.Scripts.Controllers;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private CardController cardController;
        [SerializeField] private ProgressBarHandler progressBarHandler;

        private void OnEnable()
        {
            CanvasSignals.Instance.onShowUpgradeCards += OnShowUpgradeCards;
            StackSignals.Instance.onStackDestroyed += progressBarHandler.IncreaseProgress;
        }

        private void OnShowUpgradeCards()
        {
            cardController.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            CanvasSignals.Instance.onShowUpgradeCards -= OnShowUpgradeCards;
            StackSignals.Instance.onStackDestroyed -= progressBarHandler.IncreaseProgress;
        }
    }
}