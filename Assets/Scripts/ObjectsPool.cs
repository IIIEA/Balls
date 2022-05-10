using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(BallsDataBundle ballsData)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(ballsData.GetRandomBall(), _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.First(p => p.activeSelf == false);

        return result != null;
    }
}
