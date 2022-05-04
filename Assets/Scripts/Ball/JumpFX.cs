using UnityEngine;

public class JumpFX : MonoBehaviour
{
    [SerializeField] private AnimationCurve _yAnimation;
    [SerializeField] private float _duration = 3;
    [SerializeField] private float _height = 5;

    private float _expiredTime;

    private void FixedUpdate()
    {
        StartJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _expiredTime = 0;
        }
    }

    public void StartJump()
    {
        _expiredTime += Time.deltaTime;

        if (_expiredTime > _duration)
        {
            _expiredTime = 0;
        }

        float progress = _expiredTime / _duration;

        transform.position = new Vector3(0, _yAnimation.Evaluate(progress) * _height, transform.position.z);
    }
}
