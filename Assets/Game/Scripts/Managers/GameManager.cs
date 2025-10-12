using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            GameSignals.Instance.onLevelCompleted += OnLevelCompleted;
        }

        private void OnLevelCompleted()
        {
            print("Level Completed! Handling in GameManager.");
        }

        private void OnDisable()
        {
            GameSignals.Instance.onLevelCompleted -= OnLevelCompleted;
        }
    }
}