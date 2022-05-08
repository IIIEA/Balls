using UnityEngine;
using UnityEngine.Events;

public class TouchDetector : MonoBehaviour
{
    private bool _isTouched;

    public UnityAction Touched;
    public UnityAction TouchEnd;

    private void Update()
    {
        if (Input.GetMouseButton(0) == false)
        {
            if (_isTouched)
            {
                _isTouched = false;
                TouchEnd?.Invoke();
            }

            return;
        }

        if(_isTouched == false)
        {
            _isTouched = true;
        }

        Touched?.Invoke();
    }
}
