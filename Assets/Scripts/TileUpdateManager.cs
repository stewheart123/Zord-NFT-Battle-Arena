using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileUpdateManager : MonoBehaviour
{
    [SerializeField] public Tilemap map;
    [SerializeField] public Tilemap treasureMap;

    [SerializeField] private TileBase digGroundTile;
    public List<TileBase> tesorro = new List<TileBase>();

    public Dictionary<Vector3Int, string> hiddenCollectibleCoordinates = new Dictionary<Vector3Int, string>();

    const int BOARD_X = 5;
    const int BOARD_Y = -7;
    private int treasureCount;
    [SerializeField] CollectableManager collectableManager;

    private void Start()
    {           
        HideTresure();
    }

    private void HideTresure()
    {
        treasureCount = tesorro.Count;

        List<int> positionsX = new List<int>();
        List<int> positionsY = new List<int>();

        for (int x = -5; x < BOARD_X; x++)
        {
            positionsX.Add(x);
        }
        for (int y = 1; y > BOARD_Y; y--)
        {
            positionsY.Add(y);
        }

        for (int i = 0; i < treasureCount; i++)
        {
            int totalPosXIndex = positionsX.Count;
            int totalPosYIndex = positionsY.Count;
            int randomPosX = positionsX[Random.Range(0, totalPosXIndex)];
            int randomPosY = positionsY[Random.Range(0, totalPosYIndex)];
            treasureMap.SetTile(new Vector3Int(randomPosX, randomPosY, 0), tesorro[i]);
            positionsX.Remove(randomPosX);
            positionsY.Remove(randomPosY);

        }
    }
    private void HideDictionaryCollectables()
    {
        treasureCount = tesorro.Count;
        List<int> positionsX = new List<int>();
        List<int> positionsY = new List<int>();
        //adds all possible x and y coordinates to two separate lists
        for (int x = -5; x < BOARD_X; x++)
        {
            positionsX.Add(x);
        }
        for (int y = 1; y > BOARD_Y; y--)
        {
            positionsY.Add(y);
        }
        //loops through the amount of collectables
        for (int i = 0; i < treasureCount; i++)
        {
            int totalPosXIndex = positionsX.Count;
            int totalPosYIndex = positionsY.Count;
            //selects a random entry of the x and y lists
            int randomPosX = positionsX[Random.Range(0, totalPosXIndex)];
            int randomPosY = positionsY[Random.Range(0, totalPosYIndex)];
            //adds the two to the dictionary, use the dictionary to check which image must be added to the panels when found
            hiddenCollectibleCoordinates.Add(new Vector3Int(randomPosX, randomPosY, 0), collectableManager.collectableItems[i].colectableName);
            treasureMap.SetTile(new Vector3Int(randomPosX, randomPosY, 0), collectableManager.collectableItems[i].collectableTile);

            //treasureMap.SetTile(new Vector3Int(randomPosX, randomPosY, 0), tesorro[i]);
            //removes the co ordinate option from the lists
            positionsX.Remove(randomPosX);
            positionsY.Remove(randomPosY);
        }
    }
    public void ReplaceGround(Vector3Int position)
    {
        map.SetTile(position, digGroundTile);       
    }
    public void CheckWhichTile()
    {
        //use players position against the dictionary vector3int.
        //loop through the panels and look for one with same image name
    }

}
