using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour
{
    private NavMeshAgent _agent;
    private AgentMover _mover;
    private RotationBehaviour _rotator;

    [SerializeField] private MovementIndicatorExample _indicator;

    [SerializeField] private CharacterView _characterView;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new RotationBehaviour(_rotationSpeed, transform);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetCurrentDirection(inputDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public void SetMovementFlagTo(Vector3 hit) => _indicator.SetIndicatorTo(hit);

    public void AnimateAttack()
    {
        if (_characterView != null)
        {
            _characterView.DoAttackAnimation();
        }
    }
}
