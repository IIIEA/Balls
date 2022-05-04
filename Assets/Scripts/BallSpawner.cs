using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;

    private List<GameObject> _balls = new List<GameObject>();

    public List<GameObject> Balls => _balls;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddBall();
        }
    }

    private void AddBall()
    {
        GameObject ball = Instantiate(_ballPrefab);
        _balls.Add(ball);
    }

}
