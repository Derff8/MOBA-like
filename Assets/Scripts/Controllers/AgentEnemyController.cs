using UnityEngine;
using UnityEngine.AI;

public class AgentEnemyController : Controller
{
    private AgentCharacter _character;

    private IDamageble _target;

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
        _agroRange = agroRange;
        _minDistanceToTarget = minDistanceToTarget;
        _timeToAttack = timeToAttack;
        _damage = damage;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _attackTimer -= Time.deltaTime;

        _character.SetRotationDirection(_character.CurrentVelocity);

        if (_character.TryGetPath(_target.transform.position, _pathToTarget))
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
                if (_pathToTarget.status == NavMeshPathStatus.PathPartial)
                {
                    if (distanceToTarget <= _minDistanceToTarget)
                    {
                        _character.StopMove();
                        return;
                    }
                }

                _character.ResumeMove();
                _character.SetDestination(_target.transform.position);
                return;
            }
        }

        _character.StopMove();
    }

    private void Attack()
    {
        _character.AnimateAttack();
        _target.TakeDamage(_damage);
        _attackTimer = _timeToAttack;
    }

    private bool AttackTimerIsUp() => _attackTimer < 0;

    private bool EnoughCornersInPath(NavMeshPath pathToTarget) => pathToTarget.corners.Length >= 2;

    private bool InAgroRange(float distanceToTarget)
    {
        float heightDifference = Mathf.Abs(_character.transform.position.y - _target.transform.position.y);
        return distanceToTarget <= _agroRange && heightDifference <= 1f;
    }

    private bool IsTargetRiched(float distanceToTarget)
    {
        float heightDifference = Mathf.Abs(_character.transform.position.y - _target.transform.position.y);
        return distanceToTarget <= _minDistanceToTarget && heightDifference <= 1f;
    }
}
