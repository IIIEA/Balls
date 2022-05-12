using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private ScoreTextSetter _score;

    private Slider _slider;
    private TMP_Text _text;
    
    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        _slider.value = Remap.DoRemap(_slider.minValue, _score.MaxScore, _slider.minValue, _slider.maxValue, _score.CurrentScore);
        _text.text = _score.CurrentScore + "/" + _score.MaxScore;
    }
}
