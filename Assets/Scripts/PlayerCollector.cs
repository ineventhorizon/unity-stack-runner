using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    enum Direction
    {
        Forward,
        Right, 
        Left,
    }
    private static PlayerCollector instance;
    public static PlayerCollector Instance => instance ?? (instance = FindObjectOfType<PlayerCollector>());
    [SerializeField] private Transform player;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] public float gap;
    [SerializeField] public List<Collectable> stack { set; get; }
    private Command followPlayer;
    Vector3 oldPos;
    private void Awake()
    {
        stack = new List<Collectable>();
        instance = instance ??= this;
        Debug.Log(instance);
    }
    // Update is called once per frame
    void Update()
    {
        followPlayer = new FollowPlayer(player, pivotPoint);
        ControlManager.Instance.IssueCommand(followPlayer);

        if (stack.Count != 0)
        {

            oldPos = stack[0].transform.position;
            //stack[0].position = transform.position + Vector3.forward * gap;
            stack[0].transform.position = Vector3.Lerp(transform.position + Vector3.forward * gap, oldPos, 0.8f);
            for (int i = 1; i < stack.Count; i++)
            {
                //stack[i].position = transform.position + Vector3.forward * (gap * (i+1));
                stack[i].transform.position = Vector3.Lerp(stack[i - 1].transform.position +Vector3.forward * (gap), stack[i].transform.position, 0.8f);
            }
        }
    }

    public void RemoveCollectableMultiple(Collectable collectable)
    {
        int index = stack.IndexOf(collectable);
        int lastIndex = stack.Count - 1;

        
        for (int i = index; i < lastIndex; i++)
        {
            stack[i].isCollected = false;
            stack[i].transform.localPosition += RandomPosition()* Random.Range(3, 5);
        }
        stack.RemoveRange(index, (lastIndex - index));
        
    }
    public void RemoveCollectableSingle(Collectable collectable)
    {
        stack.Remove(collectable);
        Destroy(collectable.gameObject);
    }
    public void AddToStack(Collectable collectable)
    {
        //TODO
    }

    private Vector3 RandomPosition()
    {
        //TODO
        Vector3 randomDirection = new Vector3(Random.Range(-1, 1), 0 , Random.Range(-1, 1));

        return randomDirection;
    }




}


