using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        transform.LookAt(transform.position + _target.rotation * Vector3.forward);
    }
}
