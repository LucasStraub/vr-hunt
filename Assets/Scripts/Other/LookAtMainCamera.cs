using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{
    private static Camera _cam;

    private void Start()
    {
        // Caches camera
        if (_cam == null)
            _cam = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(_cam.transform);
    }
}
