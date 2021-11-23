using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn;
    public int turnsLeft = 2;
    public int moveSpaces;
    public int actionPoints;

    public bool moveActive = false;
    public bool attackActive;
    private bool hasSwitchedGo = false;

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
    private Button digButton;
    private Button jabButton;
    private Button heavyButton;

    private Image turnPipOne;
    private Image turnPipTwo;

    private Image displayGroundTileImage;
    [SerializeField] Sprite emptyGroundImage;

    private Animator diceAnimator;
    private Animator playerAnimator;

    private TileUpdateManager tileUpdateManager;
   
    private Vector3Int playerGridPosition;

    public LayerMask collectableLayer;

    private CollectableManager collectableManager;
    private RoundManager roundManager;
    private Slider healthSlider;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        diceAnimator = GameObject.Find("Dice").GetComponent<Animator>();
        playerAnimator = gameObject.GetComponent<Animator>();

        collectableManager = GameObject.Find("Collectable Manager").GetComponent<CollectableManager>();
        roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();

        healthSlider = GameObject.Find("Panel Left/Health Slider").GetComponent<Slider>();

        moveButton = GameObject.Find("Panel Left/Move Button").GetComponent<Button>();
        actionButton = GameObject.Find("Panel Left/Action Button").GetComponent<Button>();
        moveSkipButton = GameObject.Find("Panel Left/Skip Move Button").GetComponent<Button>();
        actionSkipButton = GameObject.Find("Panel Left/Skip Action Button").GetComponent<Button>();

        moveText = GameObject.Find("Panel Left/Move Button/Move Text").GetComponent<Text>();
        actionText = GameObject.Find("Panel Left/Action Button/Action Text").GetComponent<Text>();
        turnPipOne = GameObject.Find("Panel Left/Turn Pip 1").GetComponent<Image>();
        turnPipTwo = GameObject.Find("Panel Left/Turn Pip 2").GetComponent<Image>();

        tileUpdateManager = GameObject.Find("Tile Manager").GetComponent<TileUpdateManager>();
        displayGroundTileImage = GameObject.Find("Current Dig Image").GetComponent<Image>();

        moveButton.onClick.AddListener(RollForMoves);        
        actionButton.onClick.AddListener(RollForActionPoints);        
        moveSkipButton.onClick.AddListener(SkipMoves);
        actionSkipButton.onClick.AddListener(SkipAction);

        digButton = GameObject.Find("Panel Left/Dig Button").GetComponent<Button>();
        jabButton = GameObject.Find("Panel Left/Jab Button").GetComponent<Button>();
        heavyButton = GameObject.Find("Panel Left/Heavy Button").GetComponent<Button>();

        digButton.onClick.AddListener(Dig);
        jabButton.onClick.AddListener(JabAttack);
        heavyButton.onClick.AddListener(HeavyAttack);

        digButton.interactable = false;
        jabButton.interactable = false;
        heavyButton.interactable = false;
        moveSkipButton.interactable = false;
        actionSkipButton.interactable = false;
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
           isPlayerTurn = false;
            if (!hasSwitchedGo) 
            {
                hasSwitchedGo = true;
                roundManager.SwitchPlayerIconHighlight();
                playerController.otherPlayer.GetComponent<TurnManager>().isPlayerTurn = true;
            }
            
            
            // StartCoroutine(TempcountDown());
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
            digButton.interactable = false;
            jabButton.interactable = false;
            heavyButton.interactable = false;

            if (!turnPipTwo.enabled && turnPipOne.enabled)
            {
                moveButton.interactable = true;
            }
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
        
        if(actionPoints > 4)
        {
            digButton.interactable = true;
            jabButton.interactable = true;
            heavyButton.interactable = true;
        }
        else
        {
            digButton.interactable = true;
            jabButton.interactable = true;            
        }
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
        roundManager.SwitchPlayerIconHighlight();
        Debug.Log("simulting other player go...");
        yield return new WaitForSeconds(5);
        
        turnPipTwo.enabled = true;
        turnPipOne.enabled = true;
        actionButton.interactable = true;
        moveButton.interactable = true;
        Debug.Log("...end of player 2 go");

        roundManager.SwitchPlayerIconHighlight();

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
        digButton.interactable = false;
        jabButton.interactable = false;
        heavyButton.interactable = false;
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
        if (actionPoints - dig > -1)
        {
            actionPoints = 0;
            UpdateActionPoints();

            playerGridPosition.x = Mathf.FloorToInt(gameObject.transform.position.x);            
            playerGridPosition.y = Mathf.FloorToInt(gameObject.transform.position.y);

            TileBase tileToDig = tileUpdateManager.treasureMap.GetTile(playerGridPosition);
          
            if(tileUpdateManager.treasureMap.GetSprite(playerGridPosition) != null)
            {
               displayGroundTileImage.sprite = tileUpdateManager.treasureMap.GetSprite(playerGridPosition);
               collectableManager.ShowCollectableEmblem(displayGroundTileImage.sprite.name);
                tileUpdateManager.treasureMap.SetTile(playerGridPosition, null);
            }
            else
            {
                displayGroundTileImage.sprite = emptyGroundImage;
            }
           
            playerAnimator.SetTrigger("OnDig");

           
            //if (Physics2D.OverlapCircle(transform.position, 0.1f, collectableLayer))
            //{
            //    Debug.Log("collectable found this gameobject position is... X;" + gameObject.transform.position.x + ", Y: " + gameObject.transform.position.y);

            //}

            tileUpdateManager.ReplaceGround(playerGridPosition);

        }
        else
        {
            Debug.Log("not enough action points to dig!");
         
        }

    }

    public void JabAttack()
    {
        if (actionPoints - swipe > -1)
        {
            actionPoints -= swipe;
            UpdateActionPoints();
            Debug.Log("Is swiping!!");
            playerAnimator.SetTrigger("OnDig");
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
            playerAnimator.SetTrigger("OnDig");
            UpdateActionPoints();
        }
    }
    public void HeavyAttack()
    {
        if (actionPoints - deathBlow > -1)
        {
            actionPoints -= deathBlow;
            if(Random.Range(0,2) == 1)
            {
                Debug.Log("DeathBlow hits!");
                playerAnimator.SetTrigger("OnDig");
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
    public void UpdateHealth(int damage)
    {
        healthSlider.value -= damage;
    }
    
}
