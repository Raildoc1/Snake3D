using System;
using SnakeGame.Player;
using SnakeGame.View;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SnakeGame.GameCore
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SnakeController _snakeController;
        [SerializeField] private GridSystem.Grid _grid;
        [SerializeField] private ViewManager _viewManager;
        [SerializeField] private float _tickDeltaTime = 300f;

        public UnityEvent OnSnakeDie;
        
        private Snake _snake;

        private void Awake()
        {
            _snake = new Snake(_grid);

            if (_snakeController)
            {
                _snakeController.SetSnake(_snake);
            }

            if (_viewManager)
            {
                _viewManager.Init(_grid, _snake, 1f / (_tickDeltaTime / 1000f));
            }

            _snake.Init(_grid.RequestEmptyCell(_snake));

            _grid.SpawnFood(_snake);

            PlanTick();
        }

        private void PlanTick()
        {
            Scheduler.MainThread.Schedule(TimeSpan.FromMilliseconds(_tickDeltaTime), () =>
            {
                _snake.Tick();
                if (!_snake.IsDead)
                {
                    PlanTick();
                }
                else
                {
                    OnSnakeDie?.Invoke();
                }
            });
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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