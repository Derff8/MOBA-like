using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable: ITransformPosition
{
    Vector3 CurrentVelocity { get; }

    void SetDirection(Vector3 target);
}
