using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    [SerializeField] public bool isCollected;
    [SerializeField] public int collectableLevel;
    private void Start()
    {
        this.collectableLevel = 0;
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
            AddToStack();
        }if (collision.gameObject.CompareTag("Obstacle") && isCollected)
        {
            RemoveFromStack(collision.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gate") && isCollected)
        {
            Observer.upgraded?.Invoke(this);
        }
    }

    private void AddToStack()
    {
        if (this.isCollected)
        {
            return;
        }
        Observer.collected?.Invoke(this);
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
