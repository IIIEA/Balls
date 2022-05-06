using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;

    private List<GameObject> _balls = new List<GameObject>();
    private List<Vector3> _positions = new List<Vector3>();

    public List<Vector3> Positions => _positions;
    public List<GameObject> Balls => _balls;

    private void Start()
    {
        _positions.Add(transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddBall();
        }
    }

    public void AddPosition(Vector3 followPosition)
    {
        _positions.Insert(0, followPosition);
        _positions.RemoveAt(_positions.Count - 1);
    }

    private void AddBall()
    {
        GameObject ball = Instantiate(_ballPrefab, _positions[_positions.Count - 1], Quaternion.identity, transform);
        _positions.Add(ball.transform.position);
        _balls.Add(ball);
    }
}
