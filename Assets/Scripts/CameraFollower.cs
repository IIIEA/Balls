using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _targetAfterEndLevel;
    [SerializeField] private FinishTrigger _finishTrigger;
    [SerializeField] private Restart _restart;
    [Header("Parameters")]
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _downBorder;

    private Transform _currentTarget;
    private Tweener _myTween;
    private bool _finished;
    private bool _unlockTarget;

    private void Start()
    {
        _currentTarget = _target;
    }

    private void FixedUpdate()
    {
        if (_unlockTarget == false)
        {
            Vector3 positionToGo = _currentTarget.position + _offset;

            if (positionToGo.y <= _downBorder)
            {
                positionToGo = new Vector3(positionToGo.x, _downBorder, positionToGo.z);
            }

            Vector3 smoothPosition = Vector3.Lerp(transform.position, positionToGo, _smoothSpeed);

            if (_finished)
            {
                transform.DOMove(positionToGo, 4f);
                return;
            }

            transform.position = smoothPosition;
        }
    }

    private void OnEnable()
    {
        _finishTrigger.FinishTriggered += OnFinishTriggered;
        _restart.Restarted += OnRestarted;
    }

    private void OnDisable()
    {
        _finishTrigger.FinishTriggered -= OnFinishTriggered;
        _restart.Restarted -= OnRestarted;
    }

    private void OnFinishTriggered()
    {
        StartCoroutine(ChangerTarget());
    }

    private void OnRestarted(bool restarted)
    {
        _unlockTarget = restarted;
    }

    private IEnumerator ChangerTarget()
    {
        _finished = true;
        _offset = Vector3.Lerp(_offset, new Vector3(38, 20, -18), 1f);
        _currentTarget = _targetAfterEndLevel;
        _myTween = transform.DOLookAt(new Vector3(_currentTarget.position.x - _offset.x, transform.position.y, _currentTarget.position.z + _offset.z), 3f);

        yield return _myTween.WaitForCompletion();

    }
}
