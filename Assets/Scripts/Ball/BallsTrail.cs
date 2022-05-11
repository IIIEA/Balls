using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallsTrail : ObjectsPool
{
    [Header("Links")]
    [SerializeField] private NonPhysicsJump _nonPhysicsJump;
    [SerializeField] private BallsDataBundle _ballsData;
    [SerializeField] private Transform _follow;
    [SerializeField] private Restart _restart;
    [Header("Gap")]
    [SerializeField] private float _currentGap;
    [Min(0)]
    [SerializeField] private float _minGap, _maxGap, _smoothGapSpeed;
    [Header("X position offset")]
    [SerializeField] private float _offset;

    private float _startPosX;
    private int _score = 0;

    private List<Transform> _balls = new List<Transform>();
    private List<Vector3> _positions = new List<Vector3>();

    public UnityAction<int> Score = null;

    private void OnValidate()
    {
        if(_currentGap > _maxGap || _currentGap < _minGap)
        {
            _currentGap = _maxGap;
        }
    }

    private void Start()
    {
        _positions.Add(_follow.position);
        _startPosX = _follow.position.x;
        Initialize(_ballsData);
        AddBall();
        _score = _balls.Count;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Platform>(out Platform platform))
        {
            if (platform.Value > 0)
            {
                StartCoroutine(AddBallAsync(platform.Value));
            }
            else
            {
                for (int i = 0; i < Mathf.Abs(platform.Value); i++)
                {
                    RemoveBall();
                }
            }

            _score += platform.Value;
            Score?.Invoke(_score);
        }
    }

    private void OnEnable()
    {
        _restart.ChangedYPosition += OnYPositionChanged;
    }

    private void OnDisable()
    {
        _restart.ChangedYPosition -= OnYPositionChanged;
    }

    public void BreakUp()
    {
        foreach (var ball in _balls)
        {
            ball.parent = null;

            if (ball.gameObject.TryGetComponent<Forcer>(out Forcer forcer))
            {
                forcer.StartForce();
            }
        }

        _balls.Clear();
        _positions.Clear();

        gameObject.SetActive(false);
    }

    private void Move()
    {
        if (_positions.Count > 0 && _balls.Count > 0)
        {
            float distance = (_follow.position - _positions[0]).magnitude;

            if (distance > _currentGap)
            {
                Vector3 direction = (_follow.position - _positions[0]).normalized;

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

        if(TryGetObject(out GameObject ball))
        {
            SetBall(ball, targetPosition);
        }

        _balls.Add(ball.transform);
        _positions.Add(ball.transform.position);
    }

    private void SetBall(GameObject ball, Vector3 position)
    {
        ball.SetActive(true);
        ball.transform.position = position;
    }

    private void RemoveBall()
    {
        _balls[_balls.Count - 1].gameObject.SetActive(false);
        _balls.RemoveAt(_balls.Count - 1);
        _positions.RemoveAt(_positions.Count - 1);
    }

    private void OnYPositionChanged(float newYPosition)
    {
        for (int i = 0; i < _positions.Count; i++)
        {
            _positions[i] = new Vector3(_positions[i].x, newYPosition, _positions[i].z);
        }
    }

    private IEnumerator AddBallAsync(int value)
    {
        for (int i = 0; i < value; i++)
        {
            AddBall();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
