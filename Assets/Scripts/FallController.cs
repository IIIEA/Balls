using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour
{
    [SerializeField] private float _additionalVelocity;
    [SerializeField] private float _step;

    private Rigidbody _rigidbody;

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
