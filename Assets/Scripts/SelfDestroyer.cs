using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    private float _delay;

    void Start()
    {
        Destroy(gameObject, _delay);
    }

    public void SetDelay(float delay)
    {
        _delay = delay;
    }
}
