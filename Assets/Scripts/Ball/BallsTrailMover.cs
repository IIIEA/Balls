using UnityEngine;

public class BallsTrailMover : MonoBehaviour
{
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private Transform _leaderBallPosition;
    [SerializeField] private float _gap;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var previousPosition = _leaderBallPosition.position;

        foreach (var ball in _ballSpawner.Balls)
        {
            Vector3 tempPosition = ball.transform.position;
            ball.transform.position = Vector3.Lerp(ball.transform.position, previousPosition, _gap * Time.deltaTime);
            previousPosition = tempPosition;
        }
    }
}
