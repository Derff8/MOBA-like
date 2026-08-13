using UnityEngine;

public class RotationBehaviour
{
    private Transform _transform;

    private float _rotationSpeed;

    private Vector3 _currentDirection;

    public RotationBehaviour(float rotationSpeed, Transform transform)
    {
        _rotationSpeed = rotationSpeed;
        _transform = transform;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    public void Update(float deltaTime)
    {
        if (_currentDirection.magnitude < 0.05f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_currentDirection);

        float step = _rotationSpeed * deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }

    public void SetCurrentDirection(Vector3 target) => _currentDirection = target;
}
