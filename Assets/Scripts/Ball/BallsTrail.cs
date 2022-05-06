using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsTrail : MonoBehaviour
{
    [SerializeField] private Transform _followBall;
    [SerializeField] private float _gap;

    private List<Transform> _balls = new List<Transform>();
    private List<Vector3> _positions = new List<Vector3>();

    public List<Vector3> Positions => _positions;
    public List<Transform> Balls => _balls;

    private void Start()
    {
        _positions.Add(_followBall.position);

        for (int i = 0; i < 15; i++)
        {
            AddBall();
        }
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
            _balls[i].position = Vector3.Lerp(_positions[i + 1], _positions[i], distance/_gap);
        }
    }

    private void AddBall()
    {
        Transform ball = Instantiate(_followBall, _positions[_positions.Count - 1], Quaternion.identity, transform);
        _balls.Add(ball);
        _positions.Add(ball.position);
    }
}
