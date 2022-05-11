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

    public GameObject GetBallCell(GameObject objectToFind)
    {
        var result = _ballsData.Find(p => p.BallPrefab.gameObject.name == objectToFind.name);

        return result.BallCellsPrefab;
    }
}

[Serializable]
public class BallData
{
    [SerializeField] private GameObject _ballPreefab;
    [SerializeField] private GameObject _ballCellsPrefab;

    public GameObject BallPrefab => _ballPreefab;
    public GameObject BallCellsPrefab => _ballCellsPrefab;
}
