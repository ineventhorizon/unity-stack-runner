using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform pivotPoint;
    [SerializeField] float height;
    [SerializeField] float gap;
    [SerializeField] Vector3 offset;
    [SerializeField] List<GameObject> stack;
    [SerializeField] List<Vector3> stackPositions;
    private int stackCount = 0;
    ContactPoint contact;
    private Command followPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer = new FollowPlayer(player, pivotPoint);
        ControlManager.Instance.IssueCommand(followPlayer);
    }

    void FollowPlayer()
    {
        //ControlManager.Instance.Move(this.transform, player.transform.position, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            
            CollectObject(collision.gameObject);
        }
    }

    private void CollectObject(GameObject objectToAdd)
    {
        Transform objectTransform = objectToAdd.transform;
        stack.Add(objectToAdd);
        objectTransform.SetParent(transform);
        if (stackCount == 0)
        {
            objectTransform.position = pivotPoint.position + new Vector3(0, height, gap * (stackCount + 1));
        } else
        {
            objectTransform.position = stack[stackCount-1].transform.position + new Vector3(0, 0, gap * (1));
        }
        stackCount++;
    }

    private void RemoveObject(GameObject objectToRemove)
    {
        //TODO
    }

    private void OnDrawGizmosSelected()
    {
        if (contact.point == null) return;
        Gizmos.DrawSphere(contact.point, 2);
    }
}


