using UnityEngine;

public class PlayerRotatableController : Controller
{
    private IRotatable _rotator;

    public PlayerRotatableController(IRotatable rotator)
    {
        _rotator = rotator;
    }

    private string _horizontalKey = "Horizontal";
    private string _verticalKey = "Vertical";

    protected override void UpdateLogic(float deltaTime)
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw(_horizontalKey), 0, Input.GetAxisRaw(_verticalKey));

        _rotator.SetRotation(inputDirection);
    }
}
