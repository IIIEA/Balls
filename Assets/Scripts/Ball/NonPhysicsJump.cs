using UnityEngine;

public class NonPhysicsJump : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private TouchDetector _touchDetector = null;
    [Header("Parameters")]
    [SerializeField] private float _gravity;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _rayDistance;
    [Header("Jump")]
    [Range(1,2)]
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _jumpVelocity;
    [Range(1,5)]
    [SerializeField] private float _additionJumpCoef;
    [SerializeField] private float _groundHeight;

    private Vector3 _velocity;
    private bool _isGrounded;
    private float _currentJumpVelocity;

    public float CurrentVelocity => _velocity.y;

    private void Start()
    {
        _currentJumpVelocity = _jumpVelocity;    
    }

    private void Update()
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            _gravityScale = 1;
            _velocity.y = _currentJumpVelocity;
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

        CheckGround();
    }

    private void OnEnable()
    {
        _touchDetector.Touched += OnTouched;
    }

    private void OnDisable()
    {
        _touchDetector.Touched -= OnTouched;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Platform>(out Platform platform))
        {
            _currentJumpVelocity = _jumpVelocity;
            _currentJumpVelocity += _additionJumpCoef * Mathf.Abs(platform.Value);
        }
    }

    private void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, _rayDistance, _groundMask))
        {
            if (hit.distance <= _groundHeight)
            {
                transform.position = new Vector3(0, _groundHeight, transform.position.z);
                _isGrounded = true;
            }
        }
    }

    private void OnTouched()
    {
        if (_velocity.y > 0)
        {
            _velocity.y = Mathf.Lerp(_velocity.y, 0, 10f * Time.deltaTime);
        }

        if (_gravityScale <= 4)
        {
            _gravityScale += 2 * Time.deltaTime;
        }
    }
}
