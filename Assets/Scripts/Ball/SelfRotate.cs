using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private Vector3 _startTransform;

    private void Start()
    {
        _startTransform = transform.rotation.eulerAngles;   
    }

    private void Update()
    {
        float angle = transform.eulerAngles.y;
        transform.Rotate(_rotateSpeed * Time.deltaTime, 0, 0);
    }

    private void OnDisable()
    {
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(_startTransform.x, _startTransform.y, _startTransform.z));
    }
}
