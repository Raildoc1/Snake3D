using SnakeGame.GridSystem;
using SnakeGame.Interaction;
using SnakeGame.Player;
using UnityEngine;
using Grid = SnakeGame.GridSystem.Grid;

namespace _Project.Scripts.Interaction
{
    public class Food : GridObject
    {
        public Food(Vector2Int position) : base(position)
        {
            
        }

        public override InteractableType GetType()
        {
            return InteractableType.Food;
        }

        public override void Interact(Grid grid, Snake snake)
        {
            snake.EatAt(Position);
            grid.SpawnFood(snake);
        }

    }
}