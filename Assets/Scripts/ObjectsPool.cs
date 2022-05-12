using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    public int Capacity => _capacity;

    protected void Initialize(BallsDataBundle ballsData)
    {
        for (int i = 0; i < _capacity; i++)
        {
            var ball = ballsData.GetRandomBall();
            GameObject spawned = Instantiate(ball, _container.transform);
            spawned.name = ball.name;
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.First(p => p.activeSelf == false);

        return result != null;
    }

    protected bool TryFindObject(out GameObject result, GameObject objectToFind)
    {
        result = _pool.Find(p => p == objectToFind);

        return result != null;
    }
}
