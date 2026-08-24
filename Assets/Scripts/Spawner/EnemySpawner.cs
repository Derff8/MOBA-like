using UnityEngine;

public class EnemySpawner : SpawnerExample
{
    [SerializeField] private AgentCharacter _player;
    [SerializeField] private float _agroRange;
    [SerializeField] private float _minDistanceToTarget;
    [SerializeField] private float _timeToAttack;
    [SerializeField] private float _damage;

    private void Awake()
    {
        Spawn();
    }

    protected override void InitializeSpawnedObject(GameObject spawnObject)
    {
        EnemyBrain newEnemy = spawnObject.GetComponent<EnemyBrain>();
        if (newEnemy != null)
        {
            newEnemy.Init(_player, _agroRange, _minDistanceToTarget, _timeToAttack, _damage);
        }        
    }
}
