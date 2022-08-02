using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static readonly List<SpawnPoint> SpawnPoints = new();

    private void Awake()
    {
        SpawnPoints.Add(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
