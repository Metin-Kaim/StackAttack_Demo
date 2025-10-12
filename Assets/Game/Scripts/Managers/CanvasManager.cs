using Assets.Game.Scripts.Controllers;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private CardController cardController;
        [SerializeField] private UpgradeProgressBarHandler progressBarHandler;
        [SerializeField] private GameObject startPanel;
        private void OnEnable()
        {
            CanvasSignals.Instance.onShowUpgradeCards += OnShowUpgradeCards;
            StackSignals.Instance.onStackDestroyed += progressBarHandler.IncreaseProgress;
            GameSignals.Instance.onGameStarted += OnGameStart;
        }

        private void OnGameStart()
        {
            startPanel.SetActive(false);
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
            GameSignals.Instance.onGameStarted -= OnGameStart;
        }
    }
}