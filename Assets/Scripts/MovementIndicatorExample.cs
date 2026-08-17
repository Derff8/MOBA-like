using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicatorExample : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;

    public void SetIndicatorTo(Vector3 point)
    {
        transform.position = point;
    }

}
