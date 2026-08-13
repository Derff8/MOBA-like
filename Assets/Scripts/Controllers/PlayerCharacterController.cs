using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : Controller
{
    private Character _character;

    private string _horizontalKey = "Horizontal";
    private string _verticalKey = "Vertical";

    public PlayerCharacterController(Character character)
    {
        _character = character;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw(_horizontalKey), 0, Input.GetAxisRaw(_verticalKey));

        _character.SetDirection(inputDirection);
        _character.SetRotation(inputDirection);
    }
}
