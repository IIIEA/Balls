using UnityEngine;

public class NonPhysicsJump : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private TouchDetector _touchDetector = null;
    [SerializeField] private Restart _restart;
    [Header("Parameters")]
    [SerializeField] private float _gravity;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _rayDistance;
    [Header("Jump")]
    [Range(1,2)]
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _minJumpVelocity;
    [SerializeField] private float _maxJumpVelocity;
    [SerializeField] private float _groundHeight;

    private Vector3 _velocity;
    private bool _isGrounded;
    private float _currentJumpVelocity;

    public float CurrentVelocity => _velocity.y;

    private void OnValidate()
    {
        if(_minJumpVelocity > _maxJumpVelocity)
        {
            _minJumpVelocity = _maxJumpVelocity;
        }
    }

    private void Start()
    {
        _currentJumpVelocity = _maxJumpVelocity;    
    }

    private void Update()
    {
        if (_isGrounded)
        {
            _velocity.y = 0;
            _isGrounded = false;
            _gravityScale = 1;
            _velocity.y = _currentJumpVelocity;
        }
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void OnEnable()
    {
        _touchDetector.Touched += OnTouched;
        _restart.ChangedYPosition += OnYPositionChanged;
    }

    private void OnDisable()
    {
        _touchDetector.Touched -= OnTouched;
        _restart.ChangedYPosition -= OnYPositionChanged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Platform>(out Platform platform))
        {
            _isGrounded = true;
            var velocity = Remap.DoRemap(0, 6, _minJumpVelocity, _maxJumpVelocity, Mathf.Abs(platform.Value));
            _currentJumpVelocity = Mathf.Lerp(_currentJumpVelocity, velocity, 1f);
        }
    }

    private void Jump()
    {
        Vector3 pos = transform.position;

        if (_isGrounded == false)
        {
            pos.y += _velocity.y * Time.deltaTime;
            _velocity.y += _gravity * _gravityScale * Time.deltaTime;

            transform.position = new Vector3(0, pos.y, transform.position.z);
        }
    }

    private void OnYPositionChanged(float newYPosition)
    {
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        _velocity.y = 0;
        _isGrounded = true;
    }

    private void OnTouched()
    {
        if (_velocity.y > 0)
        {
            _velocity.y = Mathf.Lerp(_velocity.y, 0, 6f * Time.deltaTime);
        }

        _gravityScale += 2 * Time.deltaTime;
    }
}
