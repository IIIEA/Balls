using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private PlatformDataBundle _platformData;
    [SerializeField] private float _spawnPosition = 0;
    [Min(0)]
    [SerializeField] private int _startPlatformsCount = 25;

    private List<Platform> _activePlatforms = new List<Platform>();

    private void Start()
    {
        for (int i = 0; i < _startPlatformsCount; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        if(_playerTransform.position.z - _startPlatformsCount > _spawnPosition - (_startPlatformsCount * _platformPrefab.transform.localScale.z))
        {
            SpawnPlatform();
            DeletePlatform();
        }
    }

    private void SpawnPlatform()
    {
        var platformData = _platformData.GetRandomData();
        var platform = Instantiate(_platformPrefab, transform.forward * _spawnPosition, transform.rotation);
        platform.Init(platformData.Value, platformData.Color);

        _activePlatforms.Add(platform);

        _spawnPosition += _platformPrefab.transform.localScale.z;
    }

    private void DeletePlatform()
    {
        Destroy(_activePlatforms[0].gameObject);
        _activePlatforms.RemoveAt(0);
    }
}
