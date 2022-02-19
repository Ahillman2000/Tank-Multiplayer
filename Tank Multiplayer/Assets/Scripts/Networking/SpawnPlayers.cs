using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject[] playerPrefabs;

    public Transform minValues;
    public Transform maxValues;
    
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Start()
    {
        minX = minValues.position.x;
        minY = minValues.position.y;

        maxX = maxValues.position.x;
        maxY = maxValues.position.y;

        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        byte _playerCount = PhotonNetwork.CurrentRoom.PlayerCount; 
        switch(_playerCount)
        {
            case 1:
                PhotonNetwork.Instantiate(playerPrefabs[0].name, randomPosition, Quaternion.identity);
                break;
            case 2:
                PhotonNetwork.Instantiate(playerPrefabs[1].name, randomPosition, Quaternion.identity);
                break;
            case 3:
                PhotonNetwork.Instantiate(playerPrefabs[2].name, randomPosition, Quaternion.identity);
                break;
            case 4:
                PhotonNetwork.Instantiate(playerPrefabs[3].name, randomPosition, Quaternion.identity);
                break;
            case 5:
                PhotonNetwork.Instantiate(playerPrefabs[4].name, randomPosition, Quaternion.identity);
                break;
            default:
                Debug.LogError("No players within room");
                break;
        }
    }

    void Update()
    {
        
    }
}
