using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HuntUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        HuntManager.OnHuntableCountChanged += HuntableCountChanged;
    }

    private void HuntableCountChanged(int value, int total)
    {
        if (_text != null)
            _text.text = $"{value}/{total}";
    }
}
