using UnityEngine;

namespace Assets.Game.Scripts.Controllers
{
    public class InputController : MonoBehaviour
    {
        private Vector2 _lastMousePosition;
        private bool _isTracking = false;

        public Vector2 MouseDelta { get; private set; }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isTracking = true;
                _lastMousePosition = Input.mousePosition;
                MouseDelta = Vector2.zero;
            }
            else if (Input.GetMouseButton(0) && _isTracking)
            {
                Vector2 currentMousePosition = Input.mousePosition;
                MouseDelta = currentMousePosition - _lastMousePosition;
                _lastMousePosition = currentMousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isTracking = false;
                MouseDelta = Vector2.zero;
            }
        }
    }
}