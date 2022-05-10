using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Forcer : MonoBehaviour
{
    [SerializeField] private SelfRotate _selfRotate;
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void StartForce()
    {
        _selfRotate.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.forward * _force, ForceMode.Impulse);
    }
}
