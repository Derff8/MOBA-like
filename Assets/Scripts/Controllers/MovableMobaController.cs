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

    public MovableMobaController(IMovable movable, NavMeshQueryFilter queryFilter)
    {
        _movable = movable;
        _queryFilter = queryFilter;
    }

    private int _groundLayerMask = LayerMask.GetMask("Ground");

    protected override void UpdateLogic(float deltaTime)
    {
        GetPathToTarget();

        FollowPath();
    }

    private void FollowPath()
    {
        if (!_isFollowingPath) return;

        if (_pathToPoint.corners.Length >= 2 && NavMeshUtils.GetPathLength(_pathToPoint) > 0.5f)
        {
            _movable.SetDirection(_pathToPoint.corners[1] - _pathToPoint.corners[0]);
            return;
        }
        _isFollowingPath = false;
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
                    _isFollowingPath = true;
                    return;
                }

                _isFollowingPath = false;
                _movable.SetDirection(Vector3.zero);
            }
        }
    }
}