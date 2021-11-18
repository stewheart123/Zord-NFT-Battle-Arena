using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CollectableItem : MonoBehaviour
{
    public Sprite collectableSprite;
    public string colectableName;
    public string collectableDescription;
    public int collectableValue;
    public string ownerAddress;
    public TileBase collectableTile;
    public bool hasBeenFound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
