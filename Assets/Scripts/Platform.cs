using TMPro;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Platform : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Renderer _renderer;

    public void Init(int value, Color color)
    {
        if (value > 0)
        {
            _value.text = "+" + value.ToString();
        }
        else
        {
            _value.text = value.ToString();
        }

        _renderer.material.color = color;
    }
}
