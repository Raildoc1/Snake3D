using System;
using System.Collections.Generic;
using SnakeGame.Interaction;
using UniRx;
using UnityEngine;

namespace SnakeGame.GridSystem
{
    public class GridView : MonoBehaviour
    {
        private Grid _grid;
        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        private Dictionary<GridObject, GameObject> _objectToViewMap = new Dictionary<GridObject, GameObject>();
        
        [SerializeField] private GameObject _foodPrefab;
        public void Init(Grid grid)
        {
            _grid = grid;
            grid.Objects.ObserveAdd().Subscribe(change => { AddObject(change.Value); }).AddTo(_disposables);
            grid.Objects.ObserveRemove().Subscribe(change => { RemoveObject(change.Value); }).AddTo(_disposables);
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
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}