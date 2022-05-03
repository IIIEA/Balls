using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private int _gap = 10;

    private List<GameObject> _balls = new List<GameObject>();
    private List<Vector3> _positions = new List<Vector3>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddBall();
        }
    }

    private void FixedUpdate()
    {
        _positions.Insert(0, transform.position);

        int index = 0;

        foreach (var ball in _balls)
        {
            Vector3 point = _positions[Mathf.Min(index * _gap, _positions.Count - 1)];
            Vector3 moveDirection = point - ball.transform.position;
            ball.transform.position += moveDirection * 10 * Time.deltaTime;
            index++;
        }
    }

    private void AddBall()
    {
        GameObject ball = Instantiate(_ballPrefab);
        _balls.Add(ball);
    }

}
