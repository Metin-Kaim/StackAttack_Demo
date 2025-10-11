using System;
using UnityEngine;

namespace Assets.Game.Scripts.Datas
{
    [Serializable]
    public class BulletData : AbsAmmunitionData
    {
        [Header("Bullet Specific")]
        public bool Piercing;
    }
    [Serializable]
    public class RocketData : AbsAmmunitionData
    {
        [Header("Rocket Specific")]
        public float ExplosionRadius;
        public float LaunchDelay;
        public float amplitude = 1f;
        public float frequency = 3f;
        public float rotationSmoothness = 10f;
    }
    [Serializable]
    public class BoomerangData : AbsAmmunitionData
    {
        [Header("Boomerang Specific")]
        public float LaunchDelay;
        public float arcHeight = 2f;
        public float rotationSpeed = 720f;
    }
    [Serializable]
    public abstract class AbsAmmunitionData
    {
        [Header("Common Properties")]
        public float duration = 2f;
        public float speed = 5f;
        public float radius = 0.2f;
        public byte damage;
        public LayerMask targetMask;
    }
}