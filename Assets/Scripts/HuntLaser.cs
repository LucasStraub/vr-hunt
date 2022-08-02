using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntLaser : MonoBehaviour
{
    [SerializeField] private Transform _laserPoint;
    [SerializeField] private Transform _laserLine;

    [SerializeField] private GameObject _effectPrefab;

    private void Awake()
    {
        HuntManager.OnHuntableCountChanged += (_, _) => Shot();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit))
        {
            if (_laserPoint != null)
            {
                _laserPoint.transform.position = hit.point;
                _laserPoint.gameObject.SetActive(true);
            }
            if (_laserLine != null)
            {
                _laserLine.LookAt(hit.point);
                _laserLine.transform.localScale = new Vector3(1, 1, hit.distance);
            }
        }
        else
        {
            if (_laserLine != null)
                _laserPoint.gameObject.SetActive(false);
        }
    }

    private void Shot()
    {
        if (_laserLine != null && _effectPrefab != null)
        {
            var effect = Instantiate(_effectPrefab, _laserLine.transform.position, _laserLine.transform.rotation);
            StartCoroutine(DestroyEffect());
            IEnumerator DestroyEffect()
            {
                yield return new WaitForSeconds(5);
                if (effect != null)
                    Destroy(effect);
            }
        }
    }
}