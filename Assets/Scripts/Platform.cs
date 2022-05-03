using TMPro;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Platform : MonoBehaviour
{
    private TMP_Text _value;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _value = GetComponentInChildren<TMP_Text>();
    }

    public void Init(int value, Color color)
    {
        _value.text = value.ToString();
        _renderer.material.color = color;
    }
}
