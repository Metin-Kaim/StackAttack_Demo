using Assets.Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Handlers
{
    public class LevelEndPanelHandler : MonoBehaviour
    {
        [SerializeField] private Button winButton;
        [SerializeField] private Button failButton;

        private void Start()
        {
            winButton.onClick.AddListener(OnPressedButton);
            failButton.onClick.AddListener(OnPressedButton);
        }

        private void OnPressedButton()
        {
            GameManager.Instance.onLoadScene.Invoke("Game");
        }
    }
}