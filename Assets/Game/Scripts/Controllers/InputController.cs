using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class InputController : MonoBehaviour
    {
        private Vector2 _lastMousePosition;
        private bool _isTouching;

        public Vector2 MouseDelta { get; private set; }
        public bool IsTouching => _isTouching;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                _lastMousePosition = Input.mousePosition;
                MouseDelta = Vector2.zero;
            }
            else if (Input.GetMouseButton(0) && _isTouching)
            {
                Vector2 currentMousePosition = Input.mousePosition;
                MouseDelta = currentMousePosition - _lastMousePosition;
                _lastMousePosition = currentMousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                MouseDelta = Vector2.zero;
            }
        }
    }
}