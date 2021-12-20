using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform pivotPoint;
    [SerializeField] float height;
    [SerializeField] float gap;
    [SerializeField] List<GameObject> stack;
    private int stackCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //FollowPlayer();
    }

    void FollowPlayer()
    {
        //ControlManager.Instance.Move(this.transform, player.transform.position, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            GameObject collidedObject = collision.gameObject;
            stack.Add(collision.gameObject);
            collidedObject.transform.SetParent(transform);
            collidedObject.transform.localPosition = pivotPoint.localPosition +new Vector3(0, height, gap * (stackCount + 1));
            stackCount++;

        }
    }
}
