using UnityEngine;

public class AgentCharacterMobaController : Controller
{
    private AgentCharacter _character;

    private int _groundLayerMask = LayerMask.GetMask("Ground");


    public AgentCharacterMobaController(AgentCharacter character)
    {
        _character = character;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _character.SetRotationDirection(_character.CurrentVelocity);

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayerMask))
            {
                _character.SetDestination(hit.point);
            }
        }
    }
}