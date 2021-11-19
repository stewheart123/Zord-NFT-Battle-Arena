using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
   [SerializeField] public GameObject spawnedPlayerOnePrefab;
    [SerializeField] public GameObject spawnedPlayerTwoPrefab;
    [SerializeField] public Transform playerOneSpawnPoint;
   [SerializeField] public Transform playerTwoSpawnPoint;
    
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        //add code to differentiate between player 1 and 2
        //  spawnedPlayerPrefab = PhotonNetwork.Instantiate("Player", transform.position, transform.rotation);

        if(GameObject.Find("Player(Clone)") == null)
        {
            Instantiate(spawnedPlayerOnePrefab, playerOneSpawnPoint.transform.position, playerOneSpawnPoint.transform.rotation);
        }
        else
        {
            Instantiate(spawnedPlayerTwoPrefab, playerTwoSpawnPoint.transform.position, playerTwoSpawnPoint.transform.rotation);
        }
        
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Player has left the room.");
        base.OnLeftRoom();
        //removes instance from server
        PhotonNetwork.Destroy(GameObject.Find("Player(Clone)"));

    }
}
