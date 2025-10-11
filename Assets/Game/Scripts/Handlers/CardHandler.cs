using Assets.Game.Scripts.Controllers;
using Assets.Game.Scripts.Datas;
using EditorAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Handlers
{
    public class CardHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private TextMeshProUGUI cardDescription;
        [SerializeField, ReadOnly] private CardData cardData;

        private CardController _cardController;
        private Button button;

        internal void FillTheCard(CardData cardData)
        {
            this.cardData = cardData;
            cardName.text = cardData.itemType.ToString();
            cardDescription.text = cardData.description;
        }

        private void Awake()
        {
            _cardController = GetComponentInParent<CardController>();
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button.onClick.AddListener(() => _cardController.ApplySelectedUpgrade(cardData));
            button.onClick.AddListener(() => _cardController.gameObject.SetActive(false));
            button.onClick.AddListener(() => Time.timeScale = 1);
        }
    }
}