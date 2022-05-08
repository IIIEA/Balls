using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] private List<float> _shakeDistance;

    private bool _isPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            if (_isPlayed == false)
                DoShake();
        }    
    }

    private void DoShake()
    {
        var startPositionY = transform.position.y;

        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < _shakeDistance.Count; i++)
        {
            if (i % 2 == 0)
            {
                sequence.Append(transform.DOMoveY(startPositionY - _shakeDistance[i], 0.2f));
            }
            else
            {
                sequence.Append(transform.DOMoveY(startPositionY + _shakeDistance[i], 0.15f));
            }
        }

        sequence.Append(transform.DOMoveY(startPositionY, 0.1f));

        _isPlayed = true;
    }
}
