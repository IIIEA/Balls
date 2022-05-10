using DG.Tweening;
using System.Collections;
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
    private bool _finished;

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

        if (_finished)
        {
            transform.DOMove(positionToGo, 2f);

            return;
        }

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
        StartCoroutine(ChangerTarget());
    }

    private IEnumerator ChangerTarget()
    {
        _finished = true;
        _offset = Vector3.Lerp(_offset, new Vector3(38, 20, -18), 100f);
        _currentTarget = _targetAfterEndLevel;

        yield return new WaitForSeconds(1.5f);

        Vector3 direction = (transform.position - _target.position).normalized;

        Debug.Log(direction);
    }
}
