using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        float angle = transform.eulerAngles.y;
        transform.Rotate(_rotateSpeed * Time.deltaTime, 0, 0);
    }
}
