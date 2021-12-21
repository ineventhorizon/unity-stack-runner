using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private static PlayerCollector instance;
    public static PlayerCollector Instance => instance ?? (instance = FindObjectOfType<PlayerCollector>());
    [SerializeField] private Transform player;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] public float gap;
    [SerializeField] public List<Transform> stack { set; get; }
    [SerializeField] public int stackCount { set; get; }
    private Command followPlayer;
    Vector3 oldPos;
    private void Awake()
    {
        stack = new List<Transform>();
        stackCount = 0;
        instance = instance ??= this;
        Debug.Log(instance);
    }
    // Update is called once per frame
    void Update()
    {
        followPlayer = new FollowPlayer(player, pivotPoint);
        ControlManager.Instance.IssueCommand(followPlayer);

        if (stackCount != 0)
        {

            oldPos = stack[0].position;
            //stack[0].position = transform.position + Vector3.forward * gap;
            stack[0].position = Vector3.Lerp(transform.position + Vector3.forward * gap, oldPos, 0.8f);
            for (int i = 1; i < stack.Count; i++)
            {
                //stack[i].position = transform.position + Vector3.forward * (gap * (i+1));
                stack[i].position = Vector3.Lerp(stack[i - 1].position +Vector3.forward * (gap), stack[i].position, 0.8f);
            }
        }
       

    }

    void FollowPlayer()
    {
        //ControlManager.Instance.Move(this.transform, player.transform.position, 0, 0);
    }

    private void RemoveObject(GameObject objectToRemove)
    {
        //TODO
    }
}


