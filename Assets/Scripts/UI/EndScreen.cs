using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private FinishTrigger _finishTrigger;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private float _yOffest;
    [SerializeField] private float _delay;
    [SerializeField] private float _showUpDuration;

    private Vector3 _startPosition;

    private void Start()
    {
        _endScreen.gameObject.SetActive(false);
        _startPosition = _endScreen.transform.position;
        _endScreen.transform.position = new Vector3(_startPosition.x, _startPosition.y - _yOffest, _startPosition.z);
        _endScreen.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void OnEnable()
    {
        _finishTrigger.FinishTriggered += OnFinishTriggired;
    }

    private void OnDisable()
    {
        _finishTrigger.FinishTriggered += OnFinishTriggired;
    }

    private void OnFinishTriggired()
    {
        StartCoroutine(TurnOn(_delay));
    }

    private IEnumerator TurnOn(float delay)
    {
        yield return new WaitForSeconds(delay);

        _endScreen.gameObject.SetActive(true);
        _endScreen.transform.DOMove(_startPosition, _showUpDuration - 0.5f);
        _endScreen.transform.DOScale(Vector3.one, _showUpDuration);
    }
}
