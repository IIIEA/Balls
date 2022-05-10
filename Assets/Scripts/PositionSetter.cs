using UnityEngine;

public class PositionSetter : MonoBehaviour
{
    [SerializeField] private LevelGenerator _level;
    [SerializeField] private Transform _transformToPlace;
    [SerializeField] private Transform _groundTransform;
    
    private void OnEnable()
    {
        _level.LastPositionSpawned += OnLastPositionSpawned;
    }

    private void OnDisable()
    {
        _level.LastPositionSpawned += OnLastPositionSpawned;
    }

    private void OnLastPositionSpawned(Vector3 position)
    {
        _transformToPlace.position = new Vector3(position.x, position.y, position.z + _groundTransform.localScale.z / 2);
        _transformToPlace.gameObject.SetActive(true);
    }
}
