using Assets.Game.Scripts.Datas.ScriptableObjects;
using Assets.Game.Scripts.Signals;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class DataController : MonoBehaviour
    {
        [SerializeField] ColorDatasSO colorDatasSO;

        private void OnEnable()
        {
            DataSignals.Instance.onGetColor += OnGetColor;
        }

        private Color OnGetColor(ColorTypes colorType)
        {
            return colorDatasSO.ColorDatas.Find(x => x.ColorType == colorType).Color;
        }

        private void OnDisable()
        {
            DataSignals.Instance.onGetColor -= OnGetColor;
        }
    }
}