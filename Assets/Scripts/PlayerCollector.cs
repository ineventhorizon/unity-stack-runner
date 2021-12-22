using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
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
            stack[0].transform.position = Vector3.Lerp(transform.position + Vector3.forward * gap, oldPos, 0.8f);
            for (int i = 1; i < stack.Count; i++)
            {
                stack[i].transform.position = Vector3.Lerp(stack[i - 1].transform.position +Vector3.forward * (gap), stack[i].transform.position, 0.8f);
            }
        }
    }

    public void RemoveCollectableMultiple(Collectable collectable, Vector3 contactPoint)
    {
        int index = stack.IndexOf(collectable);
        int lastIndex = stack.Count - 1;

        Vector3 newPos;
        for (int i = lastIndex; i >= index; i--)
        {
            newPos = stack[i].transform.position;
            newPos.x = Random.Range(0.0f, 6.0f);
            newPos.z = stack[index].transform.position.z + Random.Range(10.0f, 18.0f);

            stack[i].transform.position = newPos;
            Observer.dropped?.Invoke(stack[i]);
            stack[i].gameObject.tag = "Collectable";
            stack.RemoveAt(i);
        }
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

}


