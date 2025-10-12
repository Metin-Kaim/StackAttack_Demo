using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.Managers
{
    public class MainMenuCanvasManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;

        private void Start()
        {
            levelText.text = "Level " + PlayerPrefs.GetInt("Level", 1);
        }
    }
}