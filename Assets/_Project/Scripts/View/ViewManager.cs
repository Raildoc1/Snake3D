using System.Collections;
using System.Collections.Generic;
using SnakeGame.Grid;
using SnakeGame.Player;
using UnityEngine;

namespace SnakeGame.View
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField] private GridView _gridView;
        [SerializeField] private SnakeView _snakeView;

        public void Init(Grid.Grid grid, Snake snake)
        {
            _snakeView.Init(snake);
        }
    }
}
