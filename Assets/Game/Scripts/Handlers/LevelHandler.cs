using UnityEngine;

namespace Assets.Game.Scripts.Handlers
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private Transform levelEndPoint;

        public Transform LevelEndPoint => levelEndPoint;
    }
}