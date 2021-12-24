using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoad : MonoBehaviour
{
    [SerializeField] public List<GameObject> finalStack;
    [SerializeField] private Vector3 center;
    [SerializeField] private float radius;
    //Number of collectables each circle
    [SerializeField] private float num;
    private int count = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Stack"))
        {
            Observer.switchCam?.Invoke("FinalCam");
            Observer.banked?.Invoke(collision.collider.gameObject);
            collision.collider.gameObject.tag = "Final Road";
            Debug.Log($"Count {count} {collision.collider.name}");
            finalStack.Add(collision.collider.gameObject);
            radius = count % num == 0 ? radius - 0.25f : radius; 
            PutAroundCircle(count);
            count++;
        } if (collision.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player.");
            Observer.StopMovement?.Invoke();
        }
    }

    private void PutAroundCircle(int index)
    {
        for(int i = index; i < finalStack.Count; i++) 
        {
            var radians = 2 * Mathf.PI / num * i;
            var spawnDir = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
            finalStack[i].transform.position = this.transform.TransformPoint(center + spawnDir * radius);
        }
    }
}
