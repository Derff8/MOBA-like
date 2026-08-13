using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IMovable, IRotatable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private CharacterController _characterController;

    private MovementBehaviour _mover;
    private RotationBehaviour _rotator;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _mover = new MovementBehaviour(_moveSpeed, _characterController);
        _rotator = new RotationBehaviour(_rotationSpeed, transform);
    }

    private void Update()
    {
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void SetDirection(Vector3 inputDirection) => _mover.SetCurrentDirection(inputDirection);
    public void SetRotation(Vector3 inputDirection) => _rotator.SetCurrentDirection(inputDirection);
}
