using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class FadeImage : MonoBehaviour
{
    [SerializeField] private float _delay;

    private Image[] _images;

    private List<Image> _childImagges = new List<Image>();

    private void Awake()
    {
        _images = GetComponentsInChildren<Image>();

        for (int i = 1; i < _images.Length; i++)
        {
            _childImagges.Add(_images[i]);
        }

        foreach (var image in _childImagges)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }

    private void OnEnable()
    {
        FadeOut();
    }

    private void FadeOut()
    {
        foreach (var image in _childImagges)
        {
            image.DOFade(1, _delay);
        }
    }
}
