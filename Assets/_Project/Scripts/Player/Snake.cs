using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace SnakeGame.Player
{
    public class Snake
    {
        private ReactiveCollection<Vector2Int> _segments;
        private Grid.Grid _grid;
        private Vector2Int _direction;
        private bool _started = false;
        
        public IReadOnlyReactiveCollection<Vector2Int> Segments => _segments; 

        public Snake(Grid.Grid grid)
        {
            _grid = grid;
        }

        public void Init(Vector2Int direction)
        {
            ChangeDirection(direction);
            _segments = new ReactiveCollection<Vector2Int>();
            _segments.Add(Vector2Int.zero);
            _started = true;
        }

        public void ChangeDirection(Vector2Int direction)
        {
            _direction = direction;
        }
        
        public void ChangeDirection(InputDirection direction)
        {
            switch (direction)
            {
                case InputDirection.Left:
                    ChangeDirection(Vector2Int.left);
                    break;
                case InputDirection.Right:
                    ChangeDirection(Vector2Int.right);
                    break;
                case InputDirection.Up:
                    ChangeDirection(Vector2Int.up);
                    break;
                case InputDirection.Down:
                    ChangeDirection(Vector2Int.down);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
        
        public void Tick()
        {
            if (!_started)
            {
                return;
            }
            
            var segment = _segments.Last() + _direction;
            _segments.RemoveAt(0);
            _segments.Add(segment);

            Debug.Log("Tick");
        }
    }
}