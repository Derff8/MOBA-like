using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicatorExample : MonoBehaviour
{
    public void SetIndicatorTo(Vector3 point)
    {
        transform.position = point;
    }
}
