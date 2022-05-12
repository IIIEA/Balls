using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private FinishTrigger _finishTrigger;
    [SerializeField] private BallsTrail _ballsTrail;

    private Slider _slider;
    private TMP_Text _text;

    private void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        _finishTrigger.FinishTriggered += OnFinishTriggired;
    }

    private void OnDisable()
    {
        _finishTrigger.FinishTriggered -= OnFinishTriggired;
    }

    private void OnFinishTriggired()
    {
        _slider.value = Remap.DoRemap(_slider.minValue, _ballsTrail.Capacity, _slider.minValue, _slider.maxValue, _ballsTrail.Score);
        _text.text = _ballsTrail.Score + "/" + _ballsTrail.Capacity;
    }
}
