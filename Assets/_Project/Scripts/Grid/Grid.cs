using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Interaction;
using SnakeGame.Player;
using TMPro.EditorUtilities;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeGame.GridSystem
{
    [System.Serializable]
    public class Grid
    {
        private ReactiveCollection<GridObject> _objects = new ReactiveCollection<GridObject>();
        
        [SerializeField] private int _width = 10;
        [SerializeField] private int _height = 10;
        
        public IReadOnlyReactiveCollection<GridObject> Objects => _objects;

        public int Width => _width;
        public int Height => _height;
        
        public void MoveOnCell(Vector2Int cell, Snake snake)
        {
            foreach (var @object in Objects)
            {
                if (@object.Position.Equals(cell))
                {
                    @object.Interact(this, snake);
                    if (@object.DestroyOnInteract)
                    {
                        _objects.Remove(@object);
                    }
                    break;
                }
            }
        }

        public void SpawnFood(Snake snake)
        {
            _objects.Add(new Food(RequestEmptyCell(snake)));
        }
        
        public Vector2Int RequestEmptyCell(Snake snake)
        {
            var cell = Vector2Int.zero;
            
            do
            {
                cell.x = Random.Range(0, _width);
                cell.y = Random.Range(0, _height);
            } while (!IsCellEmpty(cell, snake));

            return cell;
        }

        public bool IsCellEmpty(Vector2Int position, Snake snake)
        {
            foreach (var obstacle in _objects)
            {
                if (position.Equals(obstacle.Position))
                {
                    return false;
                }
            }
            
            foreach (var segment in snake.Segments)
            {
                if (position.Equals(segment))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

