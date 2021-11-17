using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public bool isPlayerOneTurn;
    [SerializeField] Image playerOneIcon;
    [SerializeField] Image playerTwoIcon;

    private Color shaded;
    private Color highLighted;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerOneTurn = false;        
        shaded = new Color(0, 0, 0, 100);
        highLighted = new Color(255, 255, 255, 255);
        //playerTwoIcon.color = shaded;
        SwitchPlayerIconHighlight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SwitchPlayerIconHighlight()
    {
        if(isPlayerOneTurn)
        {
            isPlayerOneTurn = false;
            playerOneIcon.color = shaded;
            playerTwoIcon.color = highLighted;
            return;
        }
        else
        {
            isPlayerOneTurn = true;
            playerOneIcon.color = highLighted;                
            playerTwoIcon.color = shaded;
            return;
        }
        
    }

}
