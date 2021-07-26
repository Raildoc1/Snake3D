using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace SnakeGame.Player
{
    public class Snake
    {
        private ReactiveCollection<Vector2Int> _segments = new ReactiveCollection<Vector2Int> {};
        private GridSystem.Grid _grid;
        private Vector2Int _direction;
        private bool _started = false;
        private bool _directionChangeBlocked = false;
        
        public IReadOnlyReactiveCollection<Vector2Int> Segments => _segments;

        public event Action OnDie; 
        
        public bool IsDead { get; private set; } = false;

        public Snake(GridSystem.Grid grid)
        {
            _grid = grid;
        }

        public void Init(Vector2Int position)
        {
            //TryChangeDirection(direction);
            _segments.Add(position);
        }
        
        public bool TryChangeDirection(InputDirection direction)
        {
            switch (direction)
            {
                case InputDirection.Left:
                    return TryChangeDirection(Vector2Int.left);
                case InputDirection.Right:
                    return TryChangeDirection(Vector2Int.right);
                case InputDirection.Up:
                    return TryChangeDirection(Vector2Int.up);
                case InputDirection.Down:
                    return TryChangeDirection(Vector2Int.down);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private bool TryChangeDirection(Vector2Int direction)
        {
            if (_directionChangeBlocked)
            {
                return false;
            }

            var temp = _direction.x * direction.x + _direction.y * direction.y;
            var oppositeDirection = temp != 0;
            var sameDirection = temp == 1;
            
            if (sameDirection)
            {
                return false;
            }
            
            if (_segments.Count > 1 && oppositeDirection)
            {
                return false;
            }
            
            _direction = direction;
            _directionChangeBlocked = true;
            _started = true;
            return true;
        }

        public void EatAt(Vector2Int position)
        {
            _segments.Add(position);
        }

        public void Die()
        {
            IsDead = true;
            OnDie?.Invoke();
        }
        
        public void Tick()
        {
            if (!_started || IsDead)
            {
                return;
            }

            var segment = _segments.Last() + _direction;
            _segments.RemoveAt(0);
            _segments.Add(segment);
            
            _grid.MoveOnCell(segment, this);
            _directionChangeBlocked = false;
        }
    }
}