using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Grid
{
    public class GridObject
    {
        private readonly Vector2Int _position;

        public Vector2Int Position => _position;
        
        public GridObject(Vector2Int position)
        {
            _position = position;
        }
    }
}
