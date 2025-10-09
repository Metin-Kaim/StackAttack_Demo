using Assets.Game.Scripts.Signals;
using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Vector2 borderOfMovement;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float forwardSpeed = 2f;

        private void Update()
        {
            float mouseDeltaX = InputSignals.Instance.onGetMouseDeltaX.Invoke();

            Vector3 movement = moveSpeed * Time.deltaTime * new Vector3(mouseDeltaX, forwardSpeed, 0f);

            Vector3 newPosition = transform.position + movement;

            newPosition.x = Mathf.Clamp(newPosition.x, borderOfMovement.x, borderOfMovement.y);

            transform.position = newPosition;
        }
    }
}