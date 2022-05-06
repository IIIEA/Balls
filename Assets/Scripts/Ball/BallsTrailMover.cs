using System.Collections;
using UnityEngine;

public class BallsTrailMover : MonoBehaviour
{
    [SerializeField] private NonPhysicsJump _nonPhysicsJump;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _gap;

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        _nonPhysicsJump.TouchedGround += OnGroundThouched;
    }

    private void OnDisable()
    {
        _nonPhysicsJump.TouchedGround -= OnGroundThouched;
    }

    private void Move()
    {
        float sqrGap = _gap * _gap;
        var previousPosition = _followTarget.position;

        foreach (var ball in _ballSpawner.Balls)
        {
            if ((ball.transform.position - previousPosition).sqrMagnitude > sqrGap)
            {
                Vector3 tempPosition = ball.transform.position;
                ball.transform.position = previousPosition;
                previousPosition = tempPosition;
            }
            else
            {
                break;
            }
        }
    }

    private void OnGroundThouched(Vector3 groundContactPosition)
    {
        _followTarget.position = groundContactPosition;
    }
}
