using UnityEngine;

public class BackgroundBouncer : MonoBehaviour
{
    [SerializeField] private Transform _targetFollower;
    [SerializeField] private float _maxPositionChange;
    [SerializeField] private float _maxPosX;
    [SerializeField] private float _maxPosZ;

    private float _startTargetPos;
    private float _maxTargetPos;
    private float _startPosY;
    private float _startPosX;

    private void Awake()
    {
        _startTargetPos = _targetFollower.position.y;
        _maxTargetPos = _startTargetPos + _maxPositionChange;
        _startPosX = transform.position.x;
        _startPosY = transform.position.y;
    }

    private void Update()
    {
        var maxPosX = _startPosX + _maxPosX;
        var maxPosY = _startPosY + _maxPosZ;
        var newPosX = _startPosX;
        var newPosY = _startPosY;

        if (_startTargetPos <= _targetFollower.position.y)
        {
            newPosX = Remap.DoRemap(_startTargetPos, _maxTargetPos, _startPosX, maxPosX, _targetFollower.position.y);
            newPosY = Remap.DoRemap(_startTargetPos, _maxTargetPos, _startPosY, maxPosY, _targetFollower.position.y);
        }

        transform.position = new Vector3(newPosX, newPosY, _targetFollower.position.z);
    }
}
