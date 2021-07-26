using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UniRx;
using UnityEngine;

namespace SnakeGame.Grid
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
        
        public void MoveOnCell(Vector2Int cell)
        {
            foreach (var @object in Objects)
            {
                if (@object.Position.Equals(cell))
                {
                    // TODO: Interact
                }
            }
        }
        
    }
}

