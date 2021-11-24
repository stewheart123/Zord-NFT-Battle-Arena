using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using System;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    //moves before player to check if not out of bounds
    public Transform movePoint;
    public LayerMask outOfBounds;
    private Animator playerAnimator;
    private TurnManager turnManager;
    public bool isPlayerOne;
    private PhotonView photonView;
   // public bool isYourTurn;
    public GameObject otherPlayer;

    public bool hasPlayerSelectedMove = false;
    
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        movePoint.parent = null;
        playerAnimator = gameObject.GetComponent<Animator>();
        turnManager = gameObject.GetComponent<TurnManager>();
        //Debug.Log("amount of players.... " + GameObject.FindGameObjectsWithTag("Player").Length);
        Debug.Log(PhotonNetwork.PlayerList.Length + "player length");
        foreach (var person in PhotonNetwork.PlayerList)
        {
            Debug.Log("playerlist .. " + person);
        }
        if(PhotonNetwork.PlayerList.Length == 1)
        {
            isPlayerOne = true;
            turnManager.isPlayerTurn = true;
        }
        else
        {
            isPlayerOne = false;
        }


        if(isPlayerOne == true)
        {
            otherPlayer = GameObject.Find("Player 2(Clone)");
            turnManager.isPlayerTurn = true;
        }
        if (!isPlayerOne)
        {
            otherPlayer = GameObject.Find("Player(Clone)");
            turnManager.isPlayerTurn = false;
            
        }
    }

    void Update()
    {
        if (turnManager.attackActive && photonView.IsMine && turnManager.isPlayerTurn)
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
        }

        //constantly moves player towards movepoint.
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (turnManager.moveActive && turnManager.isPlayerTurn)
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
        if (photonView.IsMine && turnManager.isPlayerTurn)
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
   
}
