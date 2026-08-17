using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour
{
    [SerializeField] private AgentCharacter _playerCharacter;

    private Controller _playerController;

    private void Awake()
    {
        _playerController = new AgentCharacterMobaController(_playerCharacter);

        _playerController.Enable();
    }

    private void Update()
    {
        _playerController.Update(Time.deltaTime);
    }
}
