using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SnakeGame.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private bool _inputBlocked = false;
        private int _touchId;
        private bool _touchStarted;
        private Vector2 _touchPosition;
        
        public event Action<InputDirection> OnInputDirection;

        private void Update()
        {
            if (_inputBlocked)
            {
                return;
            }
            
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnInputDirection?.Invoke(InputDirection.Up);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                OnInputDirection?.Invoke(InputDirection.Left);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                OnInputDirection?.Invoke(InputDirection.Down);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                OnInputDirection?.Invoke(InputDirection.Right);
            }
#endif

            if (Input.touchCount != 1)
            {
                _touchStarted = false;
                return;
            }

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStarted = true;
                    _touchId = touch.fingerId;
                    _touchPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (touch.phase == TouchPhase.Began)
            {
            }

            if (!_touchStarted)
            {
                return;
            }
            
            if (Input.GetTouch(0).phase == TouchPhase.Ended && _touchId == touch.fingerId)
            {
                var delta = (touch.position - _touchPosition).normalized;

                var diagonalBasisDelta = new Vector2(delta.x + delta.y, delta.x - delta.y);
                
                if (diagonalBasisDelta.x > 0f)
                {
                    OnInputDirection?.Invoke(diagonalBasisDelta.y > 0f ? InputDirection.Right : InputDirection.Up);
                }
                else
                {
                    OnInputDirection?.Invoke(diagonalBasisDelta.y > 0f ? InputDirection.Down : InputDirection.Left);
                }
            }
            
        }
    }
}