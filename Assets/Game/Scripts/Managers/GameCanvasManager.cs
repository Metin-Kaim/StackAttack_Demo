using Assets.Game.Scripts.Controllers;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class GameCanvasManager : MonoBehaviour
    {
        [SerializeField] private CardController cardController;
        [SerializeField] private UpgradeProgressBarHandler progressBarHandler;
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject endPanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject failPanel;

        private void OnEnable()
        {
            CanvasSignals.Instance.onShowUpgradeCards += OnShowUpgradeCards;
            StackSignals.Instance.onStackDestroyed += progressBarHandler.IncreaseProgress;
            GameManager.Instance.onGameStarted += OnGameStart;
            GameManager.Instance.onLevelCompleted += OnGameWin;
            GameManager.Instance.onLevelFailed += OnGameFail;
        }

        private void OnGameFail()
        {
            OnGameEnd();
            failPanel.SetActive(true);
        }

        private void OnGameWin()
        {
            OnGameEnd();
            winPanel.SetActive(true);
        }
        private void OnGameEnd()
        {
            Time.timeScale = 0;
            endPanel.SetActive(true);
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
            GameManager.Instance.onGameStarted -= OnGameStart;
            GameManager.Instance.onLevelCompleted -= OnGameWin;
            GameManager.Instance.onLevelFailed -= OnGameFail;
        }
    }
}