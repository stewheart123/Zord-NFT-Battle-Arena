using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileUpdateManager : MonoBehaviour
{
    [SerializeField] private Tilemap map;

    [SerializeField] private TileBase digGroundTile;
    
    public void ReplaceGround(Vector3Int position)
    {
        map.SetTile(position, digGroundTile);
    }
}
