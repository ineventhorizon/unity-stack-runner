using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ControlManager : MonoBehaviour
{
    public enum MovementDirection
    {
        Forward,
        Sideways,
        Backward
    }
    private static ControlManager instance;
    public static ControlManager Instance => instance ?? (instance = FindObjectOfType<ControlManager>());
    
    private Queue<Command> queuedCommands = new Queue<Command>();
    
    // Start is called before the first frame update
    private void Awake()
    {
        instance = instance ??= this;
        Debug.Log(instance);
    }

    // Update is called once per frame
    void Update()
    {
        while(queuedCommands.Count > 0)
        {
            Command cmd = queuedCommands.Dequeue();
            cmd.ExecuteCommand();
        }
    }

    public void IssueCommand(Command cmd)
    {
        queuedCommands.Enqueue(cmd);
    }
}

public abstract class Command
{
    public abstract void ExecuteCommand();

    //public abstract void UndueCommand();
    //TODO
}

public class PlayerMove : Command
{

    private Vector3 direction;
    private Transform objectToMove;
    private float speed;
    private float sideSpeed;
    public override void ExecuteCommand()
    {
        direction = new Vector3(direction.x * sideSpeed, direction.y, direction.z * speed);
        objectToMove.position += direction * Time.deltaTime;
    }
    //public override void UndueCommand()
    //{
    //    throw new System.NotImplementedException();
    //}

    public PlayerMove(Transform _objectToMove, Vector3 _direction, float _speed, float _sideSpeed)
    {
        objectToMove = _objectToMove;
        direction = _direction;
        speed = _speed;
        sideSpeed = _sideSpeed;
    }
}
