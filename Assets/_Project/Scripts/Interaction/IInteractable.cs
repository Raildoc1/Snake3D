using SnakeGame.Player;

namespace SnakeGame.Interaction
{
    public interface IInteractable
    {
        InteractableType GetType();
        void Interact(GridSystem.Grid grid, Snake snake);
    }
}