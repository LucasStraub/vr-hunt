using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HuntManager : MonoBehaviour
{
    public static Action<int, int> OnHuntableCountChanged;

    [SerializeField] private HuntSettings _huntSettings;

    private static readonly List<Vector3> _spawnPoints = new();
    private static readonly List<Vector3> _usedSpawnPoints = new();
    private static int _huntableCount;

    private void Awake()
    {
        if (_huntSettings == null)
            _huntSettings = new();

        Huntable.OnHuntableStateChanged += HuntableStateChanged;
    }

    private void Start()
    {
        _spawnPoints.AddRange(SpawnPoint.SpawnPoints.Select(o => o.transform.position));
        SpawnHuntables();
    }

    private void SpawnHuntables()
    {
        if (_huntSettings.HuntablePrefab == null)
            return;

        _huntableCount = _huntSettings.SpawnQuantity;
        for (int i = 0; i < _huntSettings.SpawnQuantity; i++)
        {
            var huntable = Instantiate(_huntSettings.HuntablePrefab, PopRandomSpawnPoint(), Quaternion.identity, transform);
            huntable.transform.position = PopRandomSpawnPoint();
        }
        TriggerOnHuntableCountChnaged();
    }

    private Vector3 PopRandomSpawnPoint()
    {
        if (_spawnPoints.Count <= 0)
            return Vector3.zero;

        var index = UnityEngine.Random.Range(0, _spawnPoints.Count - 1);
        var spawnPoint = _spawnPoints[index];
        _spawnPoints.Remove(spawnPoint);
        _usedSpawnPoints.Add(spawnPoint);

        return spawnPoint;
    }

    private void HuntableStateChanged(HuntableBaseState state)
    {
        if (state.GetType() == typeof(HuntableCaughtState))
        {
            _huntableCount--;
            TriggerOnHuntableCountChnaged();

            if (_huntableCount <= 0)
            {
                _spawnPoints.AddRange(_usedSpawnPoints);
                _usedSpawnPoints.Clear();
                SpawnHuntables();
            }
        }
    }

    private void TriggerOnHuntableCountChnaged()
    {
        OnHuntableCountChanged?.Invoke(_huntSettings.SpawnQuantity - _huntableCount, _huntSettings.SpawnQuantity);
    }
}
