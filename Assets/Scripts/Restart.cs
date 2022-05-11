using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Restart : MonoBehaviour
{
    [SerializeField] private float _yRestartBorder;
    [SerializeField] private float _newYPosition;
    [SerializeField] private float _delay;

    private bool _isRestarted;

    public UnityAction<float> ChangedYPosition;
    public UnityAction<bool> Restarted;

    private void Start()
    {
        Restarted?.Invoke(_isRestarted);
    }

    private void Update()
    {
        if(transform.position.y < _yRestartBorder && _isRestarted == false)
        {
            _isRestarted = true;
            StartCoroutine(ResetYPositon());
        }
    }

    private IEnumerator ResetYPositon()
    {
        Restarted?.Invoke(_isRestarted);

        yield return new WaitForSeconds(_delay);

        ChangedYPosition?.Invoke(_newYPosition);
        _isRestarted = false;
        Restarted?.Invoke(_isRestarted);
    }
}
