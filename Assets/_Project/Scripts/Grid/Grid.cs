using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UniRx;
using UnityEngine;

namespace SnakeGame.Grid
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private int _width = 10;
        [SerializeField] private int _height = 10;

        public IReadOnlyReactiveCollection<GridObject> Objects => _objects;
        private ReactiveCollection<GridObject> _objects = new ReactiveCollection<GridObject>();

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

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var oldColor = Gizmos.color;
            Gizmos.color = Color.yellow;

            for (int i = 0; i <= _width; i++)
            {
                Gizmos.DrawLine(transform.position + i * Vector3.right, transform.position + _height * Vector3.forward + i * Vector3.right);
            }
            
            for (int i = 0; i <= _height; i++)
            {
                Gizmos.DrawLine(transform.position + i * Vector3.forward, transform.position + _width * Vector3.right + i * Vector3.forward);
            }
            
            Gizmos.color = oldColor;
        }
#endif
        
    }
}

