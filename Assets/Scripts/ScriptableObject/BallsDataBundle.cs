using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Balls Data Bundle", menuName = "ObjectSetting/Data/BallsDataBundle")]
public class BallsDataBundle : ScriptableObject
{
    [SerializeField] private List<BallData> _ballsData;

    public GameObject GetRandomBall()
    {
        if(_ballsData.Count != 0)
        {
            int index = UnityEngine.Random.Range(0, _ballsData.Count);

            return _ballsData[index].BallPrefab;
        }

        return null;
    }
}

[Serializable]
public class BallData
{
    [SerializeField] private GameObject _ballPreefab;

    public GameObject BallPrefab => _ballPreefab;
}
