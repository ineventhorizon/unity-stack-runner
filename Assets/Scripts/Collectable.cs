using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private bool isCollected;
    private int index;
    private Vector3 collidePoint;
    private Vector3 offset;

    private void Awake()
    {
        isCollected = false;
    }
    private void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stack") 
            || (collision.gameObject.CompareTag("Collectable")&& !isCollected))
        {
            //Debug.Log(collision.collider.transform.position);
            AddToStack(collision.collider.transform);
        }
       
    }

    private void AddToStack(Transform point)
    {

        if (isCollected)
        {
            Debug.Log("Already collected!");
            return;
        }
        index = PlayerCollector.Instance.stackCount;
        PlayerCollector.Instance.stack.Add(this.transform);
        this.isCollected = true;
        //this.transform.SetParent(PlayerCollector.Instance.transform);
        //this.gameObject.layer = 7;
        //this.transform.position = new Vector3(point.position.x, 1f, PlayerCollector.Instance.gap*index);
        Debug.Log($"{PlayerCollector.Instance.stackCount}  {point.name}----> {this.name} ");
        PlayerCollector.Instance.stackCount++;
        offset = this.transform.position;

    }

    private void RemoveFromStack()
    {
        //TODO
    }
}
