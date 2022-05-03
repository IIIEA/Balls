using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _currentTarget;
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private float _smoothSpeed = 0.125f;

    private Vector3 _offset;
    private Vector3 _targetAfterEndLevel = new Vector3(10, 0, -24);

    private void FixedUpdate()
    {
        Vector3 positionToGo = _currentTarget.position + _offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, positionToGo, _smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
        transform.LookAt(_lookTarget.position);
    }
}
