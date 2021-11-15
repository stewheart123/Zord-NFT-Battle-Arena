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

    //for testing 
    private Vector3Int moveToGridPosition;
    private Vector3Int spriteGridPosition;

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

            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                turnManager.BlockAttack();
                
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                turnManager.JabAttack();
                
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                turnManager.HeavyAttack();                
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int x = (int)gameObject.transform.position.x;
                int y = (int)gameObject.transform.position.y;
                Debug.Log("current player grid square = " + x + ", " + y );
            }
        }

        //constantly moves player towards movepoint.
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (turnManager.moveActive )
        {
            KeyPressCheck();
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
