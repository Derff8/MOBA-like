using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentEnemyController : Controller
{
    private AgentCharacter _character;

    private IDamageble _target;
    private Transform _targetTransform;

    private float _agroRange;
    private float _minDistanceToTarget;
    private float _damage;

    private float _attackTimer;
    private float _timeToAttack;

    private NavMeshPath _pathToTarget = new NavMeshPath();

    public AgentEnemyController(
        AgentCharacter character, 
        IDamageble target, 
        float agroRange, 
        float minDistanceToTarget, 
        float timeToAttack,
        float damage)
    {
        _character = character;
        _target = target;
        _targetTransform = ((MonoBehaviour)_target).transform;
        _agroRange = agroRange;
        _minDistanceToTarget = minDistanceToTarget;
        _timeToAttack = timeToAttack;
        _damage = damage;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _attackTimer -= Time.deltaTime;

        _character.SetRotationDirection(_character.CurrentVelocity);

        if (_character.TryGetPath(_targetTransform.position, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetRiched(distanceToTarget))
            {
                if (AttackTimerIsUp())
                {
                    Attack();
                }
            }                

            if (InAgroRange(distanceToTarget) && EnoughCornersInPath(_pathToTarget) && AttackTimerIsUp())
            {
                _character.ResumeMove();
                _character.SetDestination(_targetTransform.position);
                return;
            }
        }

        _character.StopMove();
    }

    private void Attack()
    {
        _target.TakeDamage(_damage);
        _attackTimer = _timeToAttack;
    }

    private bool AttackTimerIsUp() => _attackTimer < 0;

    private bool EnoughCornersInPath(NavMeshPath pathToTarget) => pathToTarget.corners.Length >= 2;

    private bool InAgroRange(float distanceToTarget) => distanceToTarget <= _agroRange;

    private bool IsTargetRiched(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
}
