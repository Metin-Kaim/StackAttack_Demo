using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private LevelHandler currentLevel;

        private void Awake()
        {
            GameObject currentLevelPrefab = Resources.Load<GameObject>("Levels/Level_" + PlayerPrefs.GetInt("Level", 1));

            currentLevel = Instantiate(currentLevelPrefab, Vector3.zero, Quaternion.identity).GetComponent<LevelHandler>();

            currentLevel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            LevelSignals.Instance.onGetCurrentLevel += OnGetCurrentLevel;
        }

        private LevelHandler OnGetCurrentLevel()
        {
            return currentLevel;
        }

        private void OnDisable()
        {
            LevelSignals.Instance.onGetCurrentLevel -= OnGetCurrentLevel;
        }

        private void Start()
        {
            currentLevel.gameObject.SetActive(true);
        }
    }
}