using UnityEngine;

public class PlayerMovableController : Controller
{
    private IMovable _movable;

    public PlayerMovableController(IMovable movable)
    {
        _movable = movable;
    }

    private string _horizontalKey = "Horizontal";
    private string _verticalKey = "Vertical";

    protected override void UpdateLogic(float deltaTime)
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw(_horizontalKey), 0, Input.GetAxisRaw(_verticalKey));

        _movable.SetDirection(inputDirection);
    }

}
