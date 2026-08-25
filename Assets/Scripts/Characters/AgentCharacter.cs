using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDamageble, IHealable
{
    private NavMeshAgent _agent;
    private AgentMover _mover;
    private RotationBehaviour _rotator;
    private Health _health;
    private AgentJumper _jumper;

    [SerializeField] private MovementIndicatorExample _indicator;

    [SerializeField] private CharacterView _characterView;

    [SerializeField] private float _jumpSpeed;
    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxHealth;
    [SerializeField] private bool _destroyAfterDeath;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public bool InJumpProcess => _jumper.InProcess;

    public bool IsDead => _health.IsDead;

    public float CurrentHealth => _health.CurrentHealth;

    private void Awake()
    {
        _health = new Health(_characterView, _maxHealth);

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new RotationBehaviour(_rotationSpeed, transform);
        _jumper = new AgentJumper(_jumpSpeed, _agent, this, _jumpCurve);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public void SetDestination(Vector3 position)
    {
        _mover.SetDestination(position);

        if (_characterView != null)
        {
            _characterView.PlaceIndicator(position);
        }
    }

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetCurrentDirection(inputDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public void AnimateAttack()
    {
        if (_characterView != null)
        {
            _characterView.DoAttackAnimation();
        }
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);

        if (CurrentHealth == 0)
            DoDieBehaviour();
    }

    public void TakeHeal(float heal)
    {
        _health.IncreaseHealth(heal);
    }

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default(OffMeshLinkData);
        return false;
    }

    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);

    private void DoDieBehaviour()
    {
        _health.SetDeadStatus();

        StopMove();

        if (_characterView != null)
        {
            _characterView.PlayDieAnimation();
        }

        if (_destroyAfterDeath)
        {
            Destroy(gameObject, 3f);
        }
    }
}
