using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Platform Data Bundle", menuName = "ObjectSetting/Data/PlatformDataBundle", order = 51)]
public class PlatformDataBundle : ScriptableObject
{
    [SerializeField] private List<PlatformData> _platformData;

    public PlatformData GetRandomData()
    {
        if (_platformData.Count != 0)
        {
            int index = UnityEngine.Random.Range(0, _platformData.Count);

            return _platformData[index];
        }

        return null;
    }
}

[Serializable]
public class PlatformData
{
    [SerializeField] private int _value;
    [SerializeField] private Color _color;

    public int Value => _value;
    public Color Color => _color;
}
