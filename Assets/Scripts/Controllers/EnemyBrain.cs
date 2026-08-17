using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private AgentCharacter _character;
    private Controller _controller;
    private Health _health;

    private void Awake()
    {
        _character = GetComponent<AgentCharacter>();
        _health = GetComponent<Health>();
    }

    public void Init(IDamageble target, float agroRange, float minDistanceToTarget, float timeToAttack, float damage)
    {
        _controller = new AgentEnemyController(_character, target, agroRange, minDistanceToTarget, timeToAttack, damage);
        _controller.Enable();
    }

    private void Update()
    {
        if (_controller  != null && !_health.IsDead)
            _controller.Update(Time.deltaTime);
    }
}
