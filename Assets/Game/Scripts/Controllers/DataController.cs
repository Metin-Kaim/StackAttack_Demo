using Assets.Game.Scripts.Datas;
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
        [SerializeField] AmmunitionModuleDatasSO ammunitionModuleDatasSO;

        private void OnEnable()
        {
            DataSignals.Instance.onGetColor += OnGetColor;
            DataSignals.Instance.onGetModuleData += OnGetModuleData;
        }

        private Color OnGetColor(ColorType colorType)
        {
            return colorDatasSO.ColorDatas.Find(x => x.ColorType == colorType).Color;
        }

        private AbsModuleData OnGetModuleData(ModuleType moduleType)
        {
            if (moduleType == ModuleType.Bullet)
                return ammunitionModuleDatasSO.AmmunitionModuleDatas.BulletModuleData;
            else if (moduleType == ModuleType.Rocket)
                return ammunitionModuleDatasSO.AmmunitionModuleDatas.RocketModuleData;
            else if (moduleType == ModuleType.Boomerang)
                return ammunitionModuleDatasSO.AmmunitionModuleDatas.BoomerangModuleData;
            else
                throw new Exception("Module Type Not Found!");
        }

        private void OnDisable()
        {
            DataSignals.Instance.onGetColor -= OnGetColor;
            DataSignals.Instance.onGetModuleData -= OnGetModuleData;
        }
    }
}