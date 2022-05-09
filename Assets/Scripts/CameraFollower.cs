using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _currentTarget;
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _downBorder;

    private Vector3 _targetAfterEndLevel = new Vector3(10, 0, -24);

    private void FixedUpdate()
    {
        Vector3 positionToGo = _currentTarget.position + _offset;

        if(positionToGo.y <= _downBorder)
        {
            positionToGo = new Vector3(positionToGo.x, _downBorder, positionToGo.z);
        }

        Vector3 smoothPosition = Vector3.Lerp(transform.position, positionToGo, _smoothSpeed);
        transform.position = smoothPosition;
    }
}
