using Assets.Game.Scripts.Managers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Handlers
{
    public class HeartPanelHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerSignals.Instance.onDecreaseHeart += DecreaseHeart;
        }
        private void OnDisable()
        {
            PlayerSignals.Instance.onDecreaseHeart -= DecreaseHeart;
        }

        private void DecreaseHeart()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(false);
                    break;
                }
            }

            bool anyHeartActive = false;

            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    anyHeartActive = true;
                    break;
                }
            }
            if (!anyHeartActive)
            {
                GameManager.Instance.onLevelFailed?.Invoke();
            }
        }
    }
}