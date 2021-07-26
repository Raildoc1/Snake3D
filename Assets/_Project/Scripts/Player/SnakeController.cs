using System;
using System.Collections;
using System.Collections.Generic;
using SnakeGame.InputSystem;
using UnityEngine;

namespace SnakeGame.Player
{
    public class SnakeController : MonoBehaviour
    {
        private Snake _snake;

        [SerializeField] private InputManager _inputManager;

        private void OnEnable()
        {
            if (!_inputManager)
            {
                _inputManager = FindObjectOfType<InputManager>();
            }

            if (!_inputManager)
            {
                Debug.LogError("No InputManager found on scene!");
                enabled = false;
                return;
            }

            _inputManager.OnInputDirection += OnDirectionInputChanged;
        }

        private void OnDisable()
        {
            _inputManager.OnInputDirection -= OnDirectionInputChanged;
        }

        public void SetSnake(Snake snake)
        {
            _snake = snake;
        }

        private void OnDirectionInputChanged(InputDirection direction)
        {
            _snake?.ChangeDirection(direction);
        }
    }
}