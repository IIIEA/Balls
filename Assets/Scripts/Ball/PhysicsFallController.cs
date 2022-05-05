using UnityEngine;

[RequireComponent(typeof(PhysicsJump))]
public class PhysicsFallController : MonoBehaviour
{
    [SerializeField] private float _step;

    private Rigidbody _rigidbody = null;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _rigidbody.velocity += Vector3.down * _step;
        }    
    }
}
