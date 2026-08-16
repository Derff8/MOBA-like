using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour
{
    [SerializeField] private AgentCharacter _playerCharacter;
    [SerializeField] private AgentCharacter _enemyCharacter;

    private Controller _playerController;
    private Controller _enemyController;

    private void Awake()
    {
        _playerController = new AgentCharacterMobaController(_playerCharacter);

        _playerController.Enable();

        IDamageble playerHealth = _playerCharacter.GetComponent<IDamageble>();

        _enemyController = new AgentEnemyController(_enemyCharacter, playerHealth, 900, 2, 1, 20);

        _enemyController.Enable();
    }

    private void Update()
    {
        _playerController.Update(Time.deltaTime);
        _enemyController.Update(Time.deltaTime);
    }
}
