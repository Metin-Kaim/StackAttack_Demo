using Assets.Game.Scripts.Handlers;
using Assets.Game.Scripts.Managers;
using Assets.Game.Scripts.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Vector2 borderOfMovement;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float forwardSpeed = 2f;

        [SerializeField] private Vector3 triggerSize;
        [SerializeField] private LayerMask hitLayer;

        private HashSet<StackHolderHandler> _hitTargets = new();


        private void Update()
        {
            if (!GameManager.Instance.onGetIsGameStarted.Invoke())
            {
                return;
            }

            float mouseDeltaX = InputSignals.Instance.onGetMouseDeltaX.Invoke();

            Vector3 movement = moveSpeed * Time.deltaTime * new Vector3(mouseDeltaX, forwardSpeed, 0f);

            Vector3 newPosition = transform.position + movement;

            newPosition.x = Mathf.Clamp(newPosition.x, borderOfMovement.x, borderOfMovement.y);

            transform.position = newPosition;

            CheckHit();
        }

        private void CheckHit()
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position, triggerSize, 0, hitLayer);

            print("Checking Hit");
            if (hit == null) return;

            print(" Hit something: " + hit.name);
            if (hit.TryGetComponent(out StackHolderHandler stack))
            {
                print("Hit " + stack.name);
                if (_hitTargets.Add(stack))
                {
                    Hit(stack);
                }
            }
        }

        private void Hit(StackHolderHandler stack)
        {
            PlayerSignals.Instance.onDecreaseHeart.Invoke();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, triggerSize);
        }
    }
}