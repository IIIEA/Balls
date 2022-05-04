using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _fallMultiplier;

    private bool _isGrounded = false;
    private Rigidbody _rigidbody;
    private float _delay = 0.1f;
    private WaitForSeconds _bufferedDeley;

    private void Start()
    {
        _bufferedDeley = new WaitForSeconds(_delay);
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isGrounded)
        {
            Physics.IgnoreLayerCollision(0, 6, true);
            StartCoroutine(IgnoreCollision());

            _isGrounded = false;
            _rigidbody.AddForce(Vector3.up * _force, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _isGrounded = true;
        }
    }

    private IEnumerator IgnoreCollision()
    {
        yield return _bufferedDeley;
        Physics.IgnoreLayerCollision(0, 6, false);
    }
}