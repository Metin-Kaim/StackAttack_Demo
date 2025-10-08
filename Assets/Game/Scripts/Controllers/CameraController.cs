using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float playerFollowYOffset = 2;

        private void LateUpdate()
        {
            float playerYPos = PlayerSignals.Instance.onGetPlayerPositionY.Invoke();

            transform.position = new Vector3(transform.position.x, playerYPos + playerFollowYOffset, transform.position.z);
        }
    }
}