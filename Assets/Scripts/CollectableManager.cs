using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
    public int indexUsed = 0;
    [SerializeField] Image[] collectableEmblems;
    
    public void ShowCollectableEmblem(int index)
    {
        collectableEmblems[index].color = Color.white;
    }
    
}
