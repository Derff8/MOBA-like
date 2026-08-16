using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
public class MovableMobaController : Controller
{
    private IMovable _movable;

    private NavMeshQueryFilter _queryFilter;

    private NavMeshPath _pathToPoint = new NavMeshPath();

    private bool _isFollowingPath = false;

    private int _groundLayerMask = LayerMask.GetMask("Ground");

    private int _currentCornerIndex = 0;

    private float _minDistanceToStop = 0.05f;

    public MovableMobaController(IMovable movable, NavMeshQueryFilter queryFilter)
    {
        _movable = movable;
        _queryFilter = queryFilter;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        GetPathToTarget();

        FollowPath();
    }

    private void FollowPath()
    {
        if (!_isFollowingPath) return;

        Vector3 currentPosition = _movable.Position;
        Vector3 targetPosition = _pathToPoint.corners[_currentCornerIndex];

        Vector3 flatCurrentPosition = new Vector3(currentPosition.x, 0, currentPosition.z);
        Vector3 flatTargetPosition = new Vector3(targetPosition.x, 0, targetPosition.z);

        if (Vector3.Distance(flatCurrentPosition, flatTargetPosition) <= _minDistanceToStop)
        {
            _currentCornerIndex++;

            if (_currentCornerIndex >= _pathToPoint.corners.Length)
            {
                _isFollowingPath = false;
                _movable.SetDirection(Vector3.zero);
                return;
            }

            targetPosition = _pathToPoint.corners[_currentCornerIndex];
            flatTargetPosition = new Vector3(targetPosition.x, 0, targetPosition.z);
        }

        Vector3 direction = (flatTargetPosition - flatCurrentPosition).normalized;
        _movable.SetDirection(direction);
    }

    private void GetPathToTarget()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayerMask))
            {
                if (NavMeshUtils.TryGetPath(_movable.Position, hit.point, _queryFilter, _pathToPoint))
                {
                    if (_pathToPoint.corners.Length > 1)
                    {
                        _isFollowingPath = true;
                        _currentCornerIndex = 1;
                        return;
                    }                    
                }

                _isFollowingPath = false;
                _movable.SetDirection(Vector3.zero);
            }
        }
    }
}