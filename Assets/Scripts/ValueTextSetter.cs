using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ValueTextSetter : MonoBehaviour
{
    private BallsTrail _ballsTrail;
    private TMP_Text _value;

    private void Awake()
    {
        _value = GetComponent<TMP_Text>();
        _ballsTrail = GetComponentInParent<BallsTrail>();
    }

    private void OnEnable()
    {
        _ballsTrail.CountBallsChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _ballsTrail.CountBallsChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        _value.text = value.ToString();
    }
}
