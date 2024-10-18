using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandInteractor : Interactor
{
    private Queue<Command> commands = new Queue<Command>();

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Camera cam;

    [SerializeField] private float destroyPointerTime = 1;
    [SerializeField] private float maxCommands = 1; // since the count starts at 0, this is 2 commands.
    private Command currentCommand;

    public override void Interact()
    {
        if (input.commandPressed)
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
            if (Physics.Raycast(ray, out var hitInfo))
            {
                if (hitInfo.transform.CompareTag("Ground") || hitInfo.transform.CompareTag("Untagged"))
                {
                    
                    if (agent.CalculatePath(hitInfo.transform.position, agent.path) && commands.Count < maxCommands) // only queue the agent if the path is possible and put the pointer down
                    {
                        GameObject pointer = Instantiate(pointerPrefab);
                        pointer.transform.position = hitInfo.point;
                        Destroy(pointer, destroyPointerTime);

                        commands.Enqueue(new MoveCommand(agent, hitInfo.point));
                    }
                }
                    else if (hitInfo.transform.CompareTag("Builder"))
                    {
                        commands.Enqueue(new BuildCommand(agent, hitInfo.transform.GetComponent<Builder>()));
                    }
            }
        }

        ProcessCommands();
    }

    private void ProcessCommands()
    {
        if (currentCommand != null && !currentCommand.isComplete)
            return;
        
        if (commands.Count == 0)
        {
            return;
        }

        currentCommand = commands.Dequeue();
        currentCommand.Execute();

    }

}
