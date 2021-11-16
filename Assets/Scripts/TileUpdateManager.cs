using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileUpdateManager : MonoBehaviour
{
    [SerializeField] public Tilemap map;
    [SerializeField] public Tilemap treasureMap;

    [SerializeField] private TileBase digGroundTile;
    public List<TileBase> tesorro = new List<TileBase>();    

    private void Start()
    {
        HideTresure();
    }

    private void HideTresure()
    {
        
        //NEED TO STOP ITEMS OVERLAPPING IF ON SAME SQUARE...
        foreach(TileBase tile in tesorro)
        {
            int randIntX = Random.Range(-5, 5);
            int randIntY = Random.Range(1, -7);

            treasureMap.SetTile(new Vector3Int(randIntX, randIntY, 0), tile);
            //map.SetTile(new Vector3Int(-5, -6, 0), tile);
            Debug.Log("tile :" + tile.name + " location:" + randIntX + " " + randIntY);
        }        

    }
    public void ReplaceGround(Vector3Int position)
    {
        map.SetTile(position, digGroundTile);       
    }
    
}
