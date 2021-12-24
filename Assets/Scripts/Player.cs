using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed, sideSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private float clampValueMin, clampValueMax;
    private bool startMovement = false;
    private Command playerMove;
    private float horizontal;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        Observer.StartMovement += MovePlayer;
        Observer.StopMovement += StopPlayer;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startMovement = !startMovement;
        } if (startMovement) Observer.StartMovement?.Invoke();
    }
    private void MovePlayer()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        direction = new Vector3(horizontal, 0, 1);
        animator.SetBool("Run", true);
        playerMove = new PlayerMove(this.transform, direction, speed, sideSpeed, clampValueMin, clampValueMax);
        ControlManager.Instance.IssueCommand(playerMove);
    }

    private void StopPlayer()
    {
        animator.SetBool("Run", false);
        playerMove = new PlayerMove(this.transform, Vector3.zero, 0, 0, 0, 6);
        startMovement = false;
        Observer.StartMovement -= MovePlayer;
        ControlManager.Instance.IssueCommand(playerMove);
    }
}
