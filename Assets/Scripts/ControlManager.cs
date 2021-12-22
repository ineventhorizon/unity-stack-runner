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
    private float clampValueMin, clampValueMax;
    private Vector3 tempPosition;
    public override void ExecuteCommand()
    {
        tempPosition = objectToMove.position;
        direction = new Vector3(direction.x * sideSpeed, direction.y, direction.z * speed);
        tempPosition.x = Mathf.Clamp(tempPosition.x, clampValueMin, clampValueMax);
        objectToMove.position = tempPosition;
        objectToMove.position += direction * Time.deltaTime ;
    }
    public PlayerMove(Transform _objectToMove, Vector3 _direction, float _speed, float _sideSpeed, float _clampValueMin, float _clampValueMax)

    {
        clampValueMin = _clampValueMin;
        clampValueMax = _clampValueMax;
        objectToMove = _objectToMove;
        direction = _direction;
        speed = _speed;
        sideSpeed = _sideSpeed;
    }
}
public class FollowPlayer : Command
{
    private Transform player;
    private Transform stack;
    public FollowPlayer(Transform _player, Transform _stack)
    {
        player = _player;
        stack = _stack;
    }
    public override void ExecuteCommand()
    {
        stack.position = Vector3.Lerp(player.position + Vector3.forward,stack.position, 0.7f);
    }
}

public class AddCollectable : Command
{
    private Transform collectable;
    private Transform newPosition;

    public AddCollectable(Transform _collectable, Transform _newPosition)
    {
        collectable = _collectable;
        newPosition = _newPosition;
    }
    public override void ExecuteCommand()
    {
        throw new System.NotImplementedException();
    }
}
