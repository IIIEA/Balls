using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallsTrail : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private NonPhysicsJump _nonPhysicsJump;
    [SerializeField] private Transform _followBall;
    [SerializeField] private BallsDataBundle _ballsData;
    [Header("Gap")]
    [SerializeField] private float _currentGap;
    [Min(0)]
    [SerializeField] private float _minGap;
    [Min(0)]
    [SerializeField] private float _maxGap;
    [Min(0)]
    [SerializeField] private float _smoothGapSpeed;
    [Header("X position offset")]
    [SerializeField] private float _offset;

    private float _startPosX;

    private List<Transform> _balls = new List<Transform>();
    private List<Vector3> _positions = new List<Vector3>();

    public UnityAction<int> CountBallsChanged = null;

    private void OnValidate()
    {
        if(_currentGap > _maxGap || _currentGap < _minGap)
        {
            _currentGap = _maxGap;
        }
    }

    private void Start()
    {
        CountBallsChanged?.Invoke(_balls.Count + 1);
        _positions.Add(_followBall.position);
        _startPosX = _followBall.position.x;
    }

    private void Update()
    {
        Move();
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

    private void Move()
    {
        float distance = (_followBall.position - _positions[0]).magnitude;

        if (distance > _currentGap)
        {
            Vector3 direction = (_followBall.position - _positions[0]).normalized;

            _positions.Insert(0, _positions[0] + direction * _currentGap);
            _positions.RemoveAt(_positions.Count - 1);

            distance -= _currentGap;
        }

        var gap = Remap.DoRemap(0, 50, _minGap, _maxGap, Mathf.Abs(_nonPhysicsJump.CurrentVelocity));
        _currentGap = Mathf.Lerp(_currentGap, gap, _smoothGapSpeed * Time.deltaTime);

        for (int i = 0; i < _balls.Count; i++)
        {
            _balls[i].position = Vector3.Lerp(new Vector3(_balls[i].position.x, _positions[i + 1].y, _positions[i + 1].z), new Vector3(_balls[i].position.x, _positions[i].y, _positions[i].z), distance / _currentGap);
        }
    }

    private void AddBall()
    {
        var newPositionX = _positions[_positions.Count - 1].x;

        if (_balls.Count > 0)
        {
            newPositionX = _balls[_balls.Count - 1].position.x + _offset;

            if (newPositionX > _startPosX + _offset)
            {
                newPositionX = _startPosX - _offset;
            }
        }

        var targetPosition = new Vector3(newPositionX, _positions[_positions.Count - 1].y, _positions[_positions.Count - 1].z);

        GameObject ball = Instantiate(_ballsData.GetRandomBall(), targetPosition, Quaternion.identity, transform);

        _balls.Add(ball.transform);
        _positions.Add(ball.transform.position);

        CountBallsChanged?.Invoke(_balls.Count + 1);
    }

    private void RemoveBall()
    {
        Destroy(_balls[_balls.Count - 1].gameObject);
        _balls.RemoveAt(_balls.Count - 1);
        _positions.RemoveAt(_positions.Count - 1);
    }
}
