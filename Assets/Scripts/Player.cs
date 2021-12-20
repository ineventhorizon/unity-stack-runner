using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed, sideSpeed;
    [SerializeField] private Animator animator;
    private bool StartMovement = false;
    private PlayerObserver.StartMovement MyMovementObserver;
    // Start is called before the first frame update
    void Start()
    {
       MyMovementObserver += MovePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartMovement = !StartMovement;
        } if (StartMovement) MyMovementObserver?.Invoke();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0, 1);
        //Debug.Log(direction);
        animator.SetBool("Run", true);
        Command playerMove = new PlayerMove(this.transform, direction, speed, sideSpeed);
        ControlManager.Instance.IssueCommand(playerMove);
    }

    
}
