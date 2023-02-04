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

        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        byte _playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.Instantiate(playerPrefabs[_playerCount].name, GenerateRandomPosition(), Quaternion.identity);
    }

    private Vector2 GenerateRandomPosition()
    {
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    void Update()
    {
        
    }
}
