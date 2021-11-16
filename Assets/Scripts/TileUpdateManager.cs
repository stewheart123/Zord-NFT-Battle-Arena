using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileUpdateManager : MonoBehaviour
{
    [SerializeField] public Tilemap map;
    [SerializeField] public Tilemap treasureMap;

    [SerializeField] private TileBase digGroundTile;
    public List<TileBase> tesorro = new List<TileBase>();

    const int BOARD_X = 5;
    const int BOARD_Y = -7;
    private int treasureCount;

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
    public void ReplaceGround(Vector3Int position)
    {
        map.SetTile(position, digGroundTile);       
    }

}
