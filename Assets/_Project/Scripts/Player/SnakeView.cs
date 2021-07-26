using System.Collections.Generic;
using System.Linq;
using SnakeGame.Player;
using UniRx;
using UnityEngine;

namespace SnakeGame.View
{
    public class SnakeView : ViewBase
    {
        private readonly List<GameObject> _segments = new List<GameObject>();
        private Snake _snake;

        [SerializeField] private GameObject _segmentPrefab;

        public void Init(Snake snake)
        {
            _snake = snake;
            _snake.Segments.ObserveAdd().Subscribe(position => { AddSegmentAt(position.Value); }).AddTo(Disposables);
            _snake.Segments.ObserveRemove().Subscribe(_ => { RemoveSegment(); }).AddTo(Disposables);
        }

        private void AddSegmentAt(Vector2Int position)
        {
            _segments.Add(Instantiate(_segmentPrefab, new Vector3(position.x + 0.5f, 0f, position.y + 0.5f), Quaternion.identity, transform));
        }

        private void RemoveSegment()
        {
            if (_segments.Count == 0)
            {
                return;
            }
            
            Destroy(_segments.First());
            _segments.RemoveAt(0);
        }
    }
}