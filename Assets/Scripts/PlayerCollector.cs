using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class PlayerCollector : MonoBehaviour
{

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
    private void Start()
    {
        Observer.collected += Collected;
        Observer.dropped += Dropped;
        Observer.upgraded += Upgrade;
        Observer.banked += Banked;
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

    public void RemoveCollectableMultiple(Collectable collectableRef, Vector3 contactPoint)
    {
        int index = stack.IndexOf(collectableRef);
        int lastIndex = stack.Count - 1;

        Vector3 newPos;
        for (int i = lastIndex; i >= index; i--)
        {
            newPos = stack[i].transform.position;
            newPos.x = Random.Range(0.0f, 6.0f);
            newPos.z = stack[index].transform.position.z + Random.Range(10.0f, 18.0f);

            stack[i].transform.position = newPos;
            Observer.dropped?.Invoke(stack[i]);
            stack.RemoveAt(i);
        }
    }
    public void RemoveCollectableSingle(Collectable collectableRef)
    {
        Observer.score?.Invoke(-10 * (collectableRef.collectableLevel + 1));
        stack.Remove(collectableRef);
        Destroy(collectableRef.gameObject);
    }
    private void Collected(Collectable collectableRef)
    {
        stack.Add(collectableRef);
        Observer.score?.Invoke(10);
        collectableRef.gameObject.tag = "Stack";
        collectableRef.isCollected = true;
    }
    private void Dropped(Collectable collectableRef)
    {
        collectableRef.gameObject.tag = "Collectable";
        Observer.score?.Invoke((collectableRef.collectableLevel + 1) * -10);
        collectableRef.isCollected = false;
    }
    private void Upgrade(Collectable collectableRef)
    {
        collectableRef.transform.GetChild(collectableRef.collectableLevel).gameObject.SetActive(false);
        collectableRef.collectableLevel = collectableRef.collectableLevel +1 >= 2 ? 2 : collectableRef.collectableLevel+1;
        collectableRef.transform.GetChild(collectableRef.collectableLevel).gameObject.SetActive(true);
        Observer.score?.Invoke(10);
    }
    private void Banked(GameObject collectableGameObj)
    {
        for(int i =stack.Count-1; i >= 0; i--)
        {
            if(stack[i].gameObject == collectableGameObj)
            {
                //stack[i].isCollected = false;
                stack.RemoveAt(i);
                return;
            }
        }
    }

}


