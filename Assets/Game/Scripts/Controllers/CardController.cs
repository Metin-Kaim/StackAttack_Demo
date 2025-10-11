using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Signals;
using EditorAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class CardController : MonoBehaviour
    {
        [SerializeField] private List<CardData> cardDatas;
        [SerializeField] private List<CardHandler> cards;
        [SerializeField, ReadOnly] private List<CardData> enableCardsDatas;

        private void Awake()
        {
            enableCardsDatas.AddRange(cardDatas.Where(x => x.itemType == ItemType.Bullet || x.upgradeData.UpgradeType == UpgradeType.Unlock).ToList());
        }

        private void OnEnable()
        {
            CardData[] selectedCardDatas = new CardData[3];

            for (int i = 0; i < 3; i++)
            {
                if (enableCardsDatas.Count == 0)
                {
                    cards[i].gameObject.SetActive(false);
                    continue;
                }

                byte rnd = (byte)Random.Range(0, enableCardsDatas.Count);
                CardData selectedCardData = enableCardsDatas[rnd];
                selectedCardDatas[i] = selectedCardData;
                enableCardsDatas.RemoveAt(rnd);
                cards[i].FillTheCard(selectedCardData);
            }
            foreach (var cardData in selectedCardDatas)
            {
                enableCardsDatas.Add(cardData);
            }
        }

        public void ApplySelectedUpgrade(CardData cardData)
        {
            if (cardData.upgradeData.UpgradeType == UpgradeType.Unlock)
            {
                PlayerSignals.Instance.onAddModule?.Invoke(cardData.upgradeData.TargetModuleType);

                enableCardsDatas.AddRange(cardDatas.Where(x => x.itemType == cardData.itemType && x.upgradeData.UpgradeType != UpgradeType.Unlock));
            }
            else
            {

                UpgradeSignals.Instance.onUpgradeApplied?.Invoke(cardData.upgradeData);
            }

            if (cardData.disposable)
            {
                enableCardsDatas.Remove(cardData);
            }
        }
    }
}