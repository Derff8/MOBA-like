using UnityEngine;

public interface IRotatable: ITransformPosition
{
    Quaternion CurrentRotation { get; }

    void SetRotation(Vector3 target);
}
