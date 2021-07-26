﻿using System;
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
        private ReactiveCollection<Vector2Int> _segments = new ReactiveCollection<Vector2Int> {};
        private GridSystem.Grid _grid;
        private Vector2Int _direction;
        private bool _started = false;

        public IReadOnlyReactiveCollection<Vector2Int> Segments => _segments;

        public Snake(GridSystem.Grid grid)
        {
            _grid = grid;
        }

        public void Init(Vector2Int direction)
        {
            TryChangeDirection(direction);
            _segments.Add(Vector2Int.zero);
            _started = true;
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
            if (_direction.x * direction.x + _direction.y * direction.y != 0)
            {
                return false;
            }
            
            _direction = direction;
            return true;
        }

        public void EatAt(Vector2Int position)
        {
            _segments.Add(position);
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
            
            _grid.MoveOnCell(segment, this);
        }
    }
}