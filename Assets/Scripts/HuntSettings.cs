using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HuntSettings", menuName = "ScriptableObjects/HuntSettings", order = 1)]
public class HuntSettings : ScriptableObject
{
    public Huntable HuntablePrefab => _huntablePrefab;
    [SerializeField] private Huntable _huntablePrefab;

    public int SpawnQuantity => _spawnQuantity;
    [SerializeField] private int _spawnQuantity = 5;

    public float CatchDistance => _catchDistance;
    [SerializeField] private float _catchDistance = 50;

    public float CatchTime => _catchTime;
    [SerializeField] private float _catchTime = 3;
}
