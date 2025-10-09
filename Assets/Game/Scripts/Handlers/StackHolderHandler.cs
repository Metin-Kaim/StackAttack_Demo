using Assets.Game.Scripts.Datas;
using UnityEngine;
using EditorAttributes;
using Assets.Game.Scripts.Signals;
using System.Collections.Generic;
using TMPro;
using System;

namespace Assets.Game.Scripts.Handlers
{
    public class StackHolderHandler : MonoBehaviour
    {
        [ReadOnly] public StackHolderConfig Config;

        [SerializeField] private List<GameObject> hexagons;
        [SerializeField] private byte health;

        private void Start()
        {
            Color color = DataSignals.Instance.onGetColor.Invoke(Config.ColorType);

            for (int i = 0; i < Config.StackSize - 1; i++)
            {
                GameObject hexagon = PoolSignals.Instance.onGetItemFromPool.Invoke(ItemTypes.Hexagon);
                hexagon.transform.SetParent(transform);
                hexagon.transform.localPosition = .1f * i * Vector3.up;
                hexagon.GetComponent<SpriteRenderer>().color = color;
                hexagons.Add(hexagon);
            }
            GameObject hexagonWText = PoolSignals.Instance.onGetItemFromPool.Invoke(ItemTypes.HexagonWithText);
            hexagonWText.transform.SetParent(transform);
            hexagonWText.transform.localPosition = .1f * hexagons.Count * Vector3.up;
            hexagonWText.GetComponent<SpriteRenderer>().color = color;
            hexagons.Add(hexagonWText);

            Config.StackText = hexagons[^1].GetComponentInChildren<TextMeshPro>();

            health = (byte)(Config.StackSize * Config.SizeMultiplier);

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

            health = (byte)Mathf.Clamp(health, 0, health);

            UpdateHealthText();

            int remainHexaCount = (int)Mathf.Floor((float)health / Config.SizeMultiplier);
            int result = hexagons.Count - remainHexaCount;
            print(result);
            if (result >= 1)
            {
                for (int i = 0; i < result; i++)
                {
                    GameObject hexa = hexagons[0];
                    hexagons.Remove(hexa);
                    Destroy(hexa);
                    transform.position += Vector3.down * .1f;
                }
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void UpdateHealthText()
        {
            Config.StackText.text = health.ToString();
        }
    }
}