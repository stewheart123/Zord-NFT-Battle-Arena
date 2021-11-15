using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn = true;
    public int turnsLeft = 2;
    public int moveSpaces;
    public int actionPoints;

    public bool moveActive = false;
    public bool attackActive;

    private int dig = 1;
    private int block = 2;
    private int swipe = 1;
    private int deathBlow = 5;

    private Text moveText;
    private Text actionText;

    private Button moveButton;
    private Button actionButton;
    private Button moveSkipButton;
    private Button actionSkipButton;

    private Image turnPipOne;
    private Image turnPipTwo;

    private Animator diceAnimator;

    private TileUpdateManager tileUpdateManager;
   
    private Vector3Int playerGridPosition;

    
    // Start is called before the first frame update
    void Start()
    {
        diceAnimator = GameObject.Find("Dice").GetComponent<Animator>();

        moveButton = GameObject.Find("Panel Left/Move Button").GetComponent<Button>();
        moveButton.onClick.AddListener(RollForMoves);
        actionButton = GameObject.Find("Panel Left/Action Button").GetComponent<Button>();
        actionButton.onClick.AddListener(RollForActionPoints);
        moveSkipButton = GameObject.Find("Panel Left/Skip Move Button").GetComponent<Button>();
        moveSkipButton.onClick.AddListener(SkipMoves);
        moveSkipButton.interactable = false;
        actionSkipButton = GameObject.Find("Panel Left/Skip Action Button").GetComponent<Button>();
        actionSkipButton.onClick.AddListener(SkipAction);
        actionSkipButton.interactable = false;
        moveText = GameObject.Find("Panel Left/Move Button/Move Text").GetComponent<Text>();
        actionText = GameObject.Find("Panel Left/Action Button/Action Text").GetComponent<Text>();
        turnPipOne = GameObject.Find("Panel Left/Turn Pip 1").GetComponent<Image>();
        turnPipTwo = GameObject.Find("Panel Left/Turn Pip 2").GetComponent<Image>();

        tileUpdateManager = GameObject.Find("Tile Manager").GetComponent<TileUpdateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RollButtonCheck();

        if(turnsLeft == 1)
        {
            turnPipTwo.enabled = false;
        }

        if (turnsLeft == 0 && moveSpaces == 0 && actionPoints == 0 )
        {   //REPLACE WITH SWITCH TO OTHER PLAYER        
            turnPipOne.enabled = false;                   
            StartCoroutine(TempcountDown());
        }
        
        if (moveSpaces == 0 && moveActive)
        {
            moveActive = false;
            moveButton.interactable = false;
            moveSkipButton.interactable = false;
            if (!turnPipTwo.enabled && turnPipOne.enabled)
            {
                actionButton.interactable = true;
            }
        }

        if(actionPoints == 0 && attackActive)
        {
            attackActive = false;
            actionButton.interactable = false;
            actionSkipButton.interactable = false;
            if (!turnPipTwo.enabled && turnPipOne.enabled)
            {
                moveButton.interactable = true;
            }
        }

        if(actionButton.interactable == false && moveButton.interactable == false)
        {
          
        }

    }
    private void RollButtonCheck()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && actionButton.interactable == true)
        {
            RollForActionPoints();
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && moveButton.interactable == true)
        {
            RollForMoves();
            return;
        }
    }

    public void RollForActionPoints()
    {
        diceAnimator.SetInteger("DiceRollNumber", 0);
        actionButton.interactable = false;
        moveButton.interactable = false;
        StartCoroutine(WaitForActionDiceRollFinish());
    }

    public void RollForMoves()
    {
        diceAnimator.SetInteger("DiceRollNumber", 0);
        actionButton.interactable = false;
        moveButton.interactable = false;
        StartCoroutine(WaitForMoveDiceRollFinish()); ;
    }

    IEnumerator WaitForActionDiceRollFinish()
    {
        diceAnimator.SetTrigger("OnRollDice");        
        yield return new WaitForSeconds(1);        
        actionPoints = Random.Range(1, 7);
        diceAnimator.SetInteger("DiceRollNumber", actionPoints);
        UpdateActionPoints();
        attackActive = true;
        turnsLeft--;
        actionSkipButton.interactable = true;
    }
    IEnumerator WaitForMoveDiceRollFinish()
    {  
        diceAnimator.SetTrigger("OnRollDice");        
        yield return new WaitForSeconds(1);
        moveSpaces = Random.Range(1, 7);
        diceAnimator.SetInteger("DiceRollNumber", moveSpaces);        
        UpdateMoveSpaces();
        moveActive = true;
        turnsLeft--;
        moveSkipButton.interactable = true;
    }
    //to be replaced with 2nd player turn...
    IEnumerator TempcountDown()
    {
        turnsLeft = 2;
        Debug.Log("simulting other player go...");
        yield return new WaitForSeconds(5);
        
        turnPipTwo.enabled = true;
        turnPipOne.enabled = true;
        actionButton.interactable = true;
        moveButton.interactable = true;
        Debug.Log("...end of player 2 go");

    }
    public void SkipMoves()
    {
        moveSpaces = 0;
        moveSkipButton.interactable = false;
        UpdateMoveSpaces();
    }
    public void SkipAction()
    {
        actionPoints = 0;
        actionSkipButton.interactable = false;
        UpdateActionPoints();
    }

    public void UpdateMoveSpaces()
    {
        moveText.text = "Moves left " + moveSpaces;
    }

    public void UpdateActionPoints()
    {
        actionText.text = "Action Points " + actionPoints ;
    }

    public void Dig()
    {
        if(actionPoints - dig > -1)
        {
            actionPoints -= dig;
            UpdateActionPoints();
            
            playerGridPosition.x = (int)gameObject.transform.position.x;
            playerGridPosition.y = (int)gameObject.transform.position.y;
            
            tileUpdateManager.ReplaceGround(playerGridPosition);
            Debug.Log("player grid position  ... " + playerGridPosition);
        }
        else 
        {
            Debug.Log("not enough action points to dig!");
        }
    }

    public void SwpieAttack()
    {
        if (actionPoints - swipe > -1)
        {
            actionPoints -= swipe;
            UpdateActionPoints();
            Debug.Log("Is swiping!!");
        }
        else
        {
            Debug.Log("not enough action points to swipe!");
        }
    }
    public void BlockAttack()
    {
        //will end the go
        if( actionPoints - block > -1)
        {
           // actionPoints -= block;
            Debug.Log("is blocking. go ended");
            actionPoints = 0;
            UpdateActionPoints();
        }
    }
    public void DeathBlowAttack()
    {
        if (actionPoints - deathBlow > -1)
        {
            actionPoints -= deathBlow;
            if(Random.Range(0,2) == 1)
            {
                Debug.Log("DeathBlow hits!");
                UpdateActionPoints();
            }
            else
            {
                Debug.Log("DeathBlow misses!");
                UpdateActionPoints();
            }
            
        }
        else
        {
            Debug.Log("not enought action points!");
        }
    }
    
}
