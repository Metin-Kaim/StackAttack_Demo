using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Datas.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ColorDatas", menuName = "StackAttack->/new ColorDatasSO")]
    public class ColorDatasSO : ScriptableObject
    {
        public List<ColorData> ColorDatas;
    }
}