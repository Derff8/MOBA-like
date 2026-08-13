using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlongMovableVelocityRotatableController : Controller
{
    private IMovable _movable;
    private IRotatable _rotatable;

    public AlongMovableVelocityRotatableController(IMovable movable, IRotatable rotatable)
    {
        _movable = movable;
        _rotatable = rotatable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _rotatable.SetRotation(_movable.CurrentVelocity);
    }
}
