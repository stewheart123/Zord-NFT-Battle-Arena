using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    //moves before player to check if not out of bounds
    public Transform movePoint;
    public LayerMask outOfBounds;
    private Animator playerAnimator;
    private TurnManager turnManager;
    private Vector3Int MoveToPosition;

    public bool hasPlayerSelectedMove = false;
    
    void Start()
    {
        movePoint.parent = null;
        playerAnimator = gameObject.GetComponent<Animator>();
        turnManager = gameObject.GetComponent<TurnManager>();           
    }

    void Update()
    {
        if (turnManager.attackActive)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                turnManager.Dig();
                playerAnimator.SetTrigger("OnDig");
                MoveToPosition.y = (int)movePoint.transform.position.y - 1;
                MoveToPosition.y = (int)movePoint.transform.position.y - 1;
                Debug.Log("move position with -1 adjust = " + MoveToPosition);
                
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                turnManager.BlockAttack();
                playerAnimator.SetTrigger("OnDig");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                turnManager.SwpieAttack();
                playerAnimator.SetTrigger("OnDig");
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                turnManager.DeathBlowAttack();
                playerAnimator.SetTrigger("OnDig");
            }
        }

        //constantly moves player towards movepoint.
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (turnManager.moveActive )
        {
            KeyPressCheck();
                //if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) )              
                //{
                //    if (!Physics2D.OverlapCircle(movePoint.position + Vector3.up , 0.1f, outOfBounds) && turnManager.moveActive)
                //    {
                //        movePoint.position +=  Vector3.up;
                //        turnManager.moveSpaces--;
                //        turnManager.UpdateMoveSpaces();
                //    }
                //}
                //if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                //{
                //    if (!Physics2D.OverlapCircle(movePoint.position + Vector3.down, 0.1f, outOfBounds) && turnManager.moveActive)
                //    {
                //        movePoint.position += Vector3.down;
                //        turnManager.moveSpaces--;
                //        turnManager.UpdateMoveSpaces();
                //    }
                //}
                //if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                //{
                //    if (!Physics2D.OverlapCircle(movePoint.position + Vector3.left, 0.1f, outOfBounds) && turnManager.moveActive)
                //    {
                //        movePoint.position += Vector3.left;
                //        turnManager.moveSpaces--;
                //        turnManager.UpdateMoveSpaces();
                //    }
                //}
                //if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                //{
                //    if (!Physics2D.OverlapCircle(movePoint.position + Vector3.right, 0.1f, outOfBounds) && turnManager.moveActive)
                //    {
                //        movePoint.position += Vector3.right;
                //        turnManager.moveSpaces--;
                //        turnManager.UpdateMoveSpaces();
                //    }
                //}

        }

        if (transform.position != movePoint.position)
        {
            playerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            playerAnimator.SetBool("IsMoving", false);
        }
       


    }
    private void KeyPressCheck()     
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (!Physics2D.OverlapCircle(movePoint.position + Vector3.up, 0.1f, outOfBounds) && turnManager.moveActive)
            {
                movePoint.position += Vector3.up;
                turnManager.moveSpaces--;
                turnManager.UpdateMoveSpaces();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (!Physics2D.OverlapCircle(movePoint.position + Vector3.down, 0.1f, outOfBounds) && turnManager.moveActive)
            {
                movePoint.position += Vector3.down;
                turnManager.moveSpaces--;
                turnManager.UpdateMoveSpaces();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (!Physics2D.OverlapCircle(movePoint.position + Vector3.left, 0.1f, outOfBounds) && turnManager.moveActive)
            {
                movePoint.position += Vector3.left;
                turnManager.moveSpaces--;
                turnManager.UpdateMoveSpaces();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (!Physics2D.OverlapCircle(movePoint.position + Vector3.right, 0.1f, outOfBounds) && turnManager.moveActive)
            {
                movePoint.position += Vector3.right;
                turnManager.moveSpaces--;
                turnManager.UpdateMoveSpaces();
            }
            return;
        }
    }
}
