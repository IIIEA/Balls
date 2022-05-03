using UnityEngine;

public class XMover : MonoBehaviour
{
    [SerializeField] private Vector3 _direction = Vector3.forward;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
