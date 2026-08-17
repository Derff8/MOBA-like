using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextExample : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Health _healthCharater;

    private void Awake()
    {
        _text.color = Color.green;
    }

    private void Update()
    {
        _text.text = "HP: " + _healthCharater.CurrentHealt.ToString();
    }
}
