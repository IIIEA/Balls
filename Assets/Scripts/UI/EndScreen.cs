using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private FinishTrigger _finishTrigger;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private float _delay;

    private Vector3 _startPosition;

    private void Start()
    {
        _endScreen.gameObject.SetActive(false);
        _startPosition = _endScreen.transform.position;
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
    }
}
