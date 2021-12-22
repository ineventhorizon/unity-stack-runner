using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    [SerializeField] public bool isCollected;
    private int Level = 0;
    private void Start()
    {
        
    }

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
        if (this.isCollected)
        {
            return;
        }
        Observer.collected?.Invoke(this);
        PlayerCollector.Instance.stack.Add(this);
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

    public void Upgrade()
    {
        //TODO
    }
}
