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
        if (collision.gameObject.CompareTag("Stack") 
            || (collision.gameObject.CompareTag("Collectable")&& !isCollected))
        {
            //Debug.Log(collision.collider.transform.position);
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
            Debug.Log("Already collected!");
            return;
        }
        PlayerCollector.Instance.stack.Add(this);
        this.isCollected = true;
        //Debug.Log($"{PlayerCollector.Instance.stack.IndexOf(this)}  {point.name}----> {this.name} ");

    }

    private void RemoveFromStack(Transform obstacleContactPoint)
    {

        Vector3 collidePoint = this.transform.position - obstacleContactPoint.position;
        //Debug.Log(test.normalized);

        if (collidePoint.z < 0) 
        {
            Debug.Log("Direct");
            PlayerCollector.Instance.RemoveCollectableSingle(this); 
        }
        else
        {
            Debug.Log("Side");
            PlayerCollector.Instance.RemoveCollectableMultiple(this);
        }
    }
}
