using System;
using System.Collections;
using System.Collections.Generic;
using SnakeGame.Player;
using SnakeGame.View;
using UniRx;
using UnityEngine;

namespace SnakeGame.GameCore
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SnakeController _snakeController;
        [SerializeField] private Grid.Grid _grid;
        [SerializeField] private ViewManager _viewManager;
        
        private Snake _snake;

        private void Awake()
        {
            _snake = new Snake(_grid);
            if (_snakeController)
            {
                _snakeController.SetSnake(_snake);
            }

            _snake.Init(Vector2Int.up);

            if (_viewManager)
            {
                _viewManager.Init(_grid, _snake);
            }
            
            PlanTick();
        }

        private void PlanTick()
        {
            Scheduler.MainThread.Schedule(TimeSpan.FromMilliseconds(700f), () =>
            {
                _snake.Tick();
                PlanTick();
            });
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var oldColor = Gizmos.color;
            Gizmos.color = Color.yellow;

            for (int i = 0; i <= _grid.Width; i++)
            {
                Gizmos.DrawLine(transform.position + i * Vector3.right,
                    transform.position + _grid.Height * Vector3.forward + i * Vector3.right);
            }

            for (int i = 0; i <= _grid.Height; i++)
            {
                Gizmos.DrawLine(transform.position + i * Vector3.forward,
                    transform.position + _grid.Width * Vector3.right + i * Vector3.forward);
            }

            Gizmos.color = oldColor;
        }
#endif
    }
}