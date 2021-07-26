using System.Collections;
using System.Collections.Generic;
using SnakeGame.GridSystem;
using SnakeGame.Player;
using UnityEngine;

namespace SnakeGame.View
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField] private GridView _gridView;
        [SerializeField] private SnakeView _snakeView;

        public void Init(GridSystem.Grid grid, Snake snake, float speed)
        {
            _snakeView.Init(snake, speed);
            _gridView.Init(grid);
        }
    }
}
