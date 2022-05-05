using UnityEngine;
using UnityEngine.Events;

public class NonPhysicsJump : MonoBehaviour
{
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _height;

    private Vector3 _velocity;
    private bool _isGrounded;

    private UnityAction TouchedGround;

    private void Update()
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            _velocity.y = _jumpVelocity;
        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        if(_isGrounded == false)
        {
            pos.y += _velocity.y * Time.deltaTime;
            _velocity.y += _gravity * Time.deltaTime;

            transform.position = new Vector3(0, pos.y, transform.position.z);
        }
    }

    private void OnEnable()
    {
        TouchedGround += OnGroundTouched;
    }

    private void OnDisable()
    {
        TouchedGround -= OnGroundTouched;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            TouchedGround?.Invoke();
        }
    }

    private void OnGroundTouched()
    {
        _isGrounded = true;
    }
}
