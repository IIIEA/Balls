using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextShowEffect : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private float _delay;

    private TMP_Text _text;
    private Color _targetColor;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        _targetColor = _text.color;

        _text.color = new Color(_startColor.r, _startColor.g, _startColor.b, 0);

    }

    private void Update()
    {
        var r = Mathf.Lerp(_text.color.r, _targetColor.r, _delay * Time.deltaTime);
        var g = Mathf.Lerp(_text.color.g, _targetColor.g, _delay * Time.deltaTime);
        var b = Mathf.Lerp(_text.color.b, _targetColor.b, _delay * Time.deltaTime);
        var a = Mathf.Lerp(_text.color.a, _targetColor.a, _delay * Time.deltaTime);
        _text.color = new Color(r, g, b, a);
    }
}
