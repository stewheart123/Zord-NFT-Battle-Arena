using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
    public int indexUsed = 0;
    [SerializeField] Image[] collectableEmblems;
    [SerializeField] public CollectableItem[] collectableItems;
    public int collectableCount;

    public void Start()
    {
        collectableCount = collectableItems.Length;

        if(collectableItems.Length != collectableEmblems.Length)
        {
            Debug.Log("mis matching list length");
        }

        for(int x = 0; x < collectableCount; x++)
        {
            collectableEmblems[x].sprite = collectableItems[x].collectableSprite;
        }
        
    }

    public void ShowCollectableEmblem(string name)
    {
        foreach(Image collectableImage in collectableEmblems)
        {
            if(collectableImage.sprite.name == name)
            {
                collectableImage.color = Color.white;
            }
        }
        foreach(CollectableItem item in collectableItems)
        {
            if(item.colectableName == name)
            {
                item.hasBeenFound = true;
            }
        }
    }
    
}
