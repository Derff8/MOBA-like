using UnityEngine;
using UnityEngine.AI;

public class InputExample : MonoBehaviour
{
    [SerializeField] private Character _character;

    private Controller _mobaController;

    private void Awake()
    {
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _mobaController = new CompositController(new MovableMobaController(_character, queryFilter), new AlongMovableVelocityRotatableController(_character, _character));

        _mobaController.Enable();
    }

    private void Update()
    {
        _mobaController.Update(Time.deltaTime);
    }
}
