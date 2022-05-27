using TMPro;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Platform : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Renderer _renderer;

    private const string ShadowColor = "_ColorDim";

    public int Value { get; private set; }

    public void Init(int value, Color color)
    {
        Value = value;

        if (value > 0)
        {
            _value.text = "+" + value.ToString();
        }
        else
        {
            _value.text = value.ToString();
        }

        _renderer.material.color = color;
        _renderer.sharedMaterial.SetColor(ShadowColor, new Color(color.r + 0.17f, color.g, color.b + 0.17f, 1f));
    }
}
