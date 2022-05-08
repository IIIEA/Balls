using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallsTrail : MonoBehaviour
{
    [SerializeField] private Transform _followBall;
    [SerializeField] private float _gap;
    [SerializeField] private float _offset;

    private List<Transform> _balls = new List<Transform>();
    private List<Vector3> _positions = new List<Vector3>();

    public UnityAction<int> CountBallsChanged = null;
    private float _startPosX;

    private void Start()
    {
        CountBallsChanged?.Invoke(_balls.Count + 1);
        _positions.Add(_followBall.position);
        _startPosX = _followBall.position.x;
    }

    private void Update()
    {
        float distance = (_followBall.position - _positions[0]).magnitude;

        if(distance > _gap)
        {
            Vector3 direction = (_followBall.position - _positions[0]).normalized;

            _positions.Insert(0, _positions[0] + direction * _gap);
            _positions.RemoveAt(_positions.Count - 1);

            distance -= _gap;
        }

        for (int i = 0; i < _balls.Count; i++)
        {
            _balls[i].position = Vector3.Lerp(_positions[i + 1], new Vector3(_positions[i + 1].x, _positions[i].y, _positions[i].z), distance / _gap);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Platform>(out Platform platform))
        {

            if (platform.Value > 0)
            {
                for (int i = 0; i < platform.Value; i++)
                {
                    AddBall();
                }
            }
            else
            {
                for (int i = 0; i < Mathf.Abs(platform.Value); i++)
                {
                    RemoveBall();
                }
            }

            CountBallsChanged?.Invoke(_balls.Count + 1);
        }
    }

    private void AddBall()
    {
        var newPositionX = _positions[_positions.Count - 1].x + _offset;

        if(newPositionX > _startPosX + _offset)
        {
            newPositionX = _startPosX - _offset;
        }

        var targetPosition = new Vector3(newPositionX, _positions[_positions.Count - 1].y, _positions[_positions.Count - 1].z);

        Transform ball = Instantiate(_followBall, targetPosition, Quaternion.identity, transform);

        _balls.Add(ball);
        _positions.Add(ball.position);

        CountBallsChanged?.Invoke(_balls.Count + 1);
    }

    private void RemoveBall()
    {
        Destroy(_balls[0].gameObject);
        _balls.RemoveAt(0);
        _positions.RemoveAt(1);
    }
}
