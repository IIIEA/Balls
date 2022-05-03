using UnityEngine;

public class XMover : MonoBehaviour
{
    [SerializeField] private Vector3 _direction = Vector3.forward;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.Translate(_direction.normalized * _speed * Time.deltaTime);
    }
}
