using UnityEngine;

public class BallWithCells : MonoBehaviour
{
    [SerializeField] private float _delay;

    private Transform[] _ballCells;

    private void Start()
    {
        _ballCells = GetComponentsInChildren<Transform>();
        BreakUpBall();
    }

    private void BreakUpBall()
    {
        foreach (var cell in _ballCells)
        {
            cell.gameObject.AddComponent<SelfDestroyer>().SetDelay(_delay);
            cell.parent = null;
        }
    }
}
