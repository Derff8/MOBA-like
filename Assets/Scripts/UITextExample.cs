using TMPro;
using UnityEngine;

public class UITextExample : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AgentCharacter _charater;

    private void Awake()
    {
        _text.color = Color.green;
    }

    private void Update()
    {
        _text.text = "HP: " + _charater.CurrentHealth.ToString();
    }
}
