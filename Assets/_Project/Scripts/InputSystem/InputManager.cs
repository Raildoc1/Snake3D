using System;
using UnityEngine;

namespace SnakeGame.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private bool _inputBlocked = false;

        public event Action<InputDirection> OnInputDirection;

        private void Update()
        {
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
        }
    }
}
