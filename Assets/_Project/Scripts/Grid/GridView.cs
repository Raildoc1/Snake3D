using System.Collections.Generic;
using SnakeGame.GridSystem;
using SnakeGame.Interaction;
using UniRx;
using UnityEngine;
using Grid = SnakeGame.GridSystem.Grid;

namespace SnakeGame.View
{
    public class GridView : ViewBase
    {
        private Grid _grid;
        private Dictionary<GridObject, GameObject> _objectToViewMap = new Dictionary<GridObject, GameObject>();
        
        [SerializeField] private GameObject _foodPrefab;
        public void Init(Grid grid)
        {
            _grid = grid;
            grid.Objects.ObserveAdd().Subscribe(change => { AddObject(change.Value); }).AddTo(Disposables);
            grid.Objects.ObserveRemove().Subscribe(change => { RemoveObject(change.Value); }).AddTo(Disposables);
        }

        private void AddObject(GridObject gridObject)
        {
            Debug.Log("AddObject");
            if (gridObject.GetType() == InteractableType.Food)
            {
                _objectToViewMap[gridObject] = Instantiate(_foodPrefab, new Vector3(gridObject.Position.x + 0.5f, 0f, gridObject.Position.y + 0.5f), Quaternion.identity, transform);
            }
        }
        
        private void RemoveObject(GridObject gridObject)
        {
            Destroy(_objectToViewMap[gridObject]);
            _objectToViewMap.Remove(gridObject);
        }
    }
}