using UnityEngine;

public class JumpFX : MonoBehaviour
{
    [SerializeField] private AnimationCurve _yAnimation;
    [SerializeField] private float _duration = 3;
    [SerializeField] private float _height = 5;

    private float _expiredTime;
    private Keyframe[] _yKeyFrames;

    private void Start()
    {
        _yKeyFrames = _yAnimation.keys;
    }

    private void Update()
    {
        StartJump();
    }

    private void OnTriggerEnter(Collider other)
{
        if (other.gameObject.layer == 6)
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
