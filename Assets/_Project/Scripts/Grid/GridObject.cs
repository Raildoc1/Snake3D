using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using SnakeGame.Interaction;
using SnakeGame.Player;
using SnakeGame.View;
using UnityEngine;

namespace SnakeGame.GridSystem
{
    public class GridObject : IInteractable
    {
        public bool DestroyOnInteract { get; private set; }
        public Vector2Int Position { get; set; }
        
        public GridObject(Vector2Int position, bool destroyOnInteract = true)
        {
            Position = position;
            DestroyOnInteract = destroyOnInteract;
        }

        public virtual InteractableType GetType()
        {
            return InteractableType.None;
        }

        public virtual void Interact(Grid grid, Snake snake)
        {
        }
        
    }
}
