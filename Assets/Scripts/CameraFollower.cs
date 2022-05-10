using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _targetAfterEndLevel;
    [SerializeField] private FinishTrigger _finishTrigger;
    [Header("Parameters")]
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _downBorder;

    private Transform _currentTarget;

    private void Start()
    {
        _currentTarget = _target;
    }

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

    private void OnEnable()
    {
        _finishTrigger.FinishTriggered += OnFinishTriggered;
    }

    private void OnDisable()
    {
        _finishTrigger.FinishTriggered -= OnFinishTriggered;
    }

    private void OnFinishTriggered()
    {
        _currentTarget = _targetAfterEndLevel;
    }
}
