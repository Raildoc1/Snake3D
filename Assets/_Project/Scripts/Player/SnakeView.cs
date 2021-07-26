using System.Collections;
using System.Collections.Generic;
using SnakeGame.Player;
using UniRx;
using UnityEngine;

namespace SnakeGame.View
{
    public class SnakeView : ViewBase
    {
        private readonly List<GameObject> _segments = new List<GameObject>();
        private Snake _snake;
        private float _speed;
        private bool _stopped = false;
        
        [SerializeField] private GameObject _segmentPrefab;

        public void Init(Snake snake, float speed)
        {
            _snake = snake;
            _speed = speed;
            _snake.Segments.ObserveAdd().Subscribe(position => { AddSegmentAt(position.Value); }).AddTo(Disposables);
            _snake.OnDie += OnSnakeDie;
        }

        private void OnDisable()
        {
            if (_snake == null)
            {
                return;
            }
            
            _snake.OnDie -= OnSnakeDie;
        }

        private void AddSegmentAt(Vector2Int position)
        {
            if (_segments.Count < _snake.Segments.Count)
            {
                StartCoroutine(SpawnSegmentRoutine(1f / _speed, position));
            }
        }

        private IEnumerator SpawnSegmentRoutine(float time, Vector2Int position)
        {
            yield return new WaitForSeconds(time);
            _segments.Add(Instantiate(_segmentPrefab, new Vector3(position.x + 0.5f, 0f, position.y + 0.5f),
                Quaternion.identity, transform));
        }

        private void Update()
        {
            if (_stopped)
            {
                return;
            }
            
            var targetPosition = Vector3.zero;
            
            for (int i = 0; i < Mathf.Min(_segments.Count, _snake.Segments.Count); i++)
            {
                targetPosition.x = _snake.Segments[i].x + 0.5f;
                targetPosition.z = _snake.Segments[i].y + 0.5f;
                _segments[i].transform.position = Vector3.MoveTowards(_segments[i].transform.position, targetPosition,
                    _speed * Time.deltaTime);
            }
        }

        private void OnSnakeDie()
        {
            StartCoroutine(DieRoutine(0.3f / _speed));
        }

        private IEnumerator DieRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            _stopped = true;
        }
    }
}