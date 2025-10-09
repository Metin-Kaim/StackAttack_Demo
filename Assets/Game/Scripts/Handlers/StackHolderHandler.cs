using Assets.Game.Scripts.Datas;
using Assets.Game.Scripts.Signals;
using DG.Tweening;
using EditorAttributes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.STP;

namespace Assets.Game.Scripts.Handlers
{
    public class StackHolderHandler : MonoBehaviour
    {
        [ReadOnly] public StackHolderConfig Config;

        [SerializeField] private List<GameObject> hexagons;
        [SerializeField] private byte health;

        private Vector3 _initScale;
        private byte MaxHealth;

        private void Start()
        {
            _initScale = transform.localScale;

            Color color = DataSignals.Instance.onGetColor.Invoke(Config.ColorType);

            for (int i = 0; i < Config.StackSize - 1; i++)
            {
                GameObject hexagon = PoolSignals.Instance.onGetItemFromPool.Invoke(ItemTypes.Hexagon);
                hexagon.transform.SetParent(transform);
                hexagon.transform.localPosition = .1f * i * Vector3.up;
                SpriteRenderer sprite = hexagon.GetComponent<SpriteRenderer>();
                sprite.color = color;
                sprite.sortingOrder = i;
                hexagons.Add(hexagon);
            }
            GameObject hexagonWText = PoolSignals.Instance.onGetItemFromPool.Invoke(ItemTypes.HexagonWithText);
            hexagonWText.transform.SetParent(transform);
            hexagonWText.transform.localPosition = .1f * hexagons.Count * Vector3.up;
            SpriteRenderer sprite2 = hexagonWText.GetComponent<SpriteRenderer>();
            sprite2.color = color;
            sprite2.sortingOrder = hexagons.Count;
            hexagons.Add(hexagonWText);

            Config.StackText = hexagons[^1].GetComponentInChildren<TextMeshPro>();
            Config.StackText.sortingOrder = hexagons.Count;

            health = (byte)(Config.StackSize * Config.SizeMultiplier);
            MaxHealth = health;

            UpdateHealthText();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector2.one);
        }

        public void Hit(byte damage)
        {
            health -= damage;

            if (health > MaxHealth)
            {
                health = 0;
            }
            //print(health);
            if (health <= 0)
            {
                Destroy(gameObject);
                return;
            }

            UpdateHealthText();

            int remainHexaCount = (int)Mathf.Ceil((float)health / Config.SizeMultiplier);
            int destroyableHexaCount = hexagons.Count - remainHexaCount;

            if (destroyableHexaCount >= 1)
            {
                for (int i = 0; i < destroyableHexaCount; i++)
                {
                    GameObject hexa = hexagons[0];
                    hexagons.Remove(hexa);
                    Destroy(hexa);
                    transform.position += Vector3.down * .1f;
                }
                transform.DOComplete();
                transform.DOScale(_initScale + Vector3.one * 0.1f, 0.2f).SetLoops(2, LoopType.Yoyo).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
            }
        }

        private void UpdateHealthText()
        {
            Config.StackText.text = health.ToString();
        }
    }
}