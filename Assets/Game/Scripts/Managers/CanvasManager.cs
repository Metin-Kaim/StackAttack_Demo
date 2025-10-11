using Assets.Game.Scripts.Controllers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private CardController cardController;

        private void OnEnable()
        {
            CanvasSignals.Instance.onShowUpgradeCards += OnShowUpgradeCards;
        }

        private void OnShowUpgradeCards()
        {
            cardController.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            CanvasSignals.Instance.onShowUpgradeCards -= OnShowUpgradeCards;
        }
    }
}