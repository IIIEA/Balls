using UnityEngine;
using UnityEngine.Events;

public class NonPhysicsJump : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private TouchDetector _touchDetector = null;
    [Header("Parameters")]
    [SerializeField] private float _gravity;
    [Range(1,2)]
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _height;

    private Vector3 _velocity;
    private bool _isGrounded;

    public UnityAction<Vector3> TouchedGround;

    private void Update()
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            _gravityScale = 1;
            _velocity.y = _jumpVelocity;
        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        if(_isGrounded == false)
        {
            pos.y += _velocity.y * Time.deltaTime;
            _velocity.y += _gravity * _gravityScale * Time.deltaTime;

            transform.position = new Vector3(0, pos.y, transform.position.z);
        }

        if(transform.position.y <= 1f)
        {
            transform.position = new Vector3(0, 1f, transform.position.z);
            _isGrounded = true;
        }
    }

    private void OnEnable()
    {
        _touchDetector.Touched += OnTouched;
        TouchedGround += OnGroundTouched;
    }

    private void OnDisable()
    {
        _touchDetector.Touched -= OnTouched;
        TouchedGround -= OnGroundTouched;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            TouchedGround?.Invoke(transform.position);
        }
    }

    private void OnTouched()
    {

        if (_velocity.y > 0)
        {
            _velocity.y = Mathf.Lerp(_velocity.y, 0, 0.1f * Time.deltaTime);
        }

        if (_gravityScale <= 4)
        {
            _gravityScale += 2 * Time.deltaTime;
        }

    }

    private void OnGroundTouched(Vector3 groundContactPosition)
    {
        _isGrounded = true;
    }
}
