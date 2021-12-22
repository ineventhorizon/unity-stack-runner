using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public bool isCollected;


    private void Awake()
    {
        isCollected = false;
    }
    private void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stack"))
        {
            AddToStack(collision.collider.transform);
        }if (collision.gameObject.CompareTag("Obstacle") && isCollected)
        {
            RemoveFromStack(collision.transform);
        }
    }

    private void AddToStack(Transform point)
    {
        if (isCollected)
        {
            return;
        }
        this.gameObject.tag = "Stack";
        PlayerCollector.Instance.stack.Add(this);
        this.isCollected = true;
    }

    private void RemoveFromStack(Transform obstacleContactPoint)
    {
        Vector3 collidePoint = this.transform.position - obstacleContactPoint.position;
        if (collidePoint.z < 0) 
        {
            //Direct
            PlayerCollector.Instance.RemoveCollectableSingle(this); 
        }
        else
        {
            //Side
            PlayerCollector.Instance.RemoveCollectableMultiple(this, obstacleContactPoint.localPosition);
        }
    }
}
