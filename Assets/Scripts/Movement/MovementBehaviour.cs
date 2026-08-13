using UnityEngine;

public class MovementBehaviour
{
    private CharacterController _characterController;

    private float _moveSpeed;

    private Vector3 _currentDirection;

    public MovementBehaviour(float moveSpeed, CharacterController controller)
    {
        _moveSpeed = moveSpeed;
        _characterController = controller;
    }

    public Vector3 CurrentVelocity { get; private set; }

    public void Update(float deltaTime)
    {
        CurrentVelocity = _currentDirection.normalized * _moveSpeed;

        _characterController.Move(CurrentVelocity * deltaTime);
    }

    public void SetCurrentDirection(Vector3 target) => _currentDirection = target;
}
