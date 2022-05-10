using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private FinishTrigger _finish;

    private Forcer _forcer;

    private void Start()
    {
        _forcer = GetComponent<Forcer>();    
    }

    private void OnEnable()
    {
        _finish.FinishTriggered += OnFinishTriggered;
    }

    private void OnDisable()
    {
        _finish.FinishTriggered -= OnFinishTriggered;
    }

    private void OnFinishTriggered()
    {
        transform.parent = null;
        _forcer.StartForce();
    }
}
