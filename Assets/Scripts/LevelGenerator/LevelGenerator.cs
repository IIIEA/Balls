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
    [Min(1)]
    [SerializeField] private int _countStages = 4;
    [Range(1,3)]
    [SerializeField] private int _spaceBetweenStages = 2;
    [Range(0,10)]
    [SerializeField] private int _negativePlatformChance;
    [SerializeField] private int _chanceAddStep;

    private List<Platform> _activePlatforms = new List<Platform>();

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < _countStages; i++)
        {
            for (int j = 0; j < _startPlatformsCount; j++)
            {
                SpawnPlatform(_negativePlatformChance);
            }

            _negativePlatformChance += _chanceAddStep;
            _spawnPosition += _platformPrefab.transform.localScale.z * _spaceBetweenStages;
        }
    }

    private void SpawnPlatform(int chance)
    {
        var allRandom = Random.Range(1,101);

        var platform = Instantiate(_platformPrefab, transform.forward * _spawnPosition, _platformPrefab.transform.rotation, transform);

        PlatformData platformData;

        if(allRandom <= chance)
        {
            do
            {
                platformData = _platformData.GetRandomData();
            } 
            while (platformData.Value >= 0);
        }
        else
        {
            do
            {
                platformData = _platformData.GetRandomData();
            }
            while (platformData.Value <= 0);
        }

        platform.Init(platformData.Value, platformData.Color);

        _activePlatforms.Add(platform);

        _spawnPosition += platform.transform.lossyScale.x * 2;
    }
}
