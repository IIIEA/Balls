using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Platform Data Bundle", menuName = "ObjectSetting/Data/PlatformDataBundle", order = 51)]
public class PlatformDataBundle : ScriptableObject
{
    [SerializeField] private List<PlatformData> _platformsData;

    public List<PlatformData> PlatformsData => _platformsData;
}

[Serializable]
public class PlatformData
{
    [SerializeField] private int _value;
    [SerializeField] private Color _color;

    public int Value => _value;
    public Color Color => _color;
}
