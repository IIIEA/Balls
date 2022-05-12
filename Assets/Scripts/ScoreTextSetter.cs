using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreTextSetter : MonoBehaviour
{
    private BallsTrail _ballsTrail;
    private TMP_Text _value;

    public float MaxScore { get; private set; }
    public float CurrentScore { get; private set; }

    private void Awake()
    {
        _value = GetComponent<TMP_Text>();
        _ballsTrail = GetComponentInParent<BallsTrail>();
        MaxScore = _ballsTrail.Capacity;
    }

    private void OnEnable()
    {
        _ballsTrail.ScoreChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _ballsTrail.ScoreChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _value.text = value.ToString();
        CurrentScore = value;
    }
}
