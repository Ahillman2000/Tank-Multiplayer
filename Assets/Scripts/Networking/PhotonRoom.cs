using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    // Room Info
    public static PhotonRoom Instance { get; private set; }
    //private PhotonView pView;

    public bool isGameLoaded;
    public int currentSceneIndex;

    // player Info
    private Player[] photonPlayers;
    public int numberOfPlayersInRoom;
    public int myNumberInRoom;

    public int playersInGame;

    // Delayed Start
    private bool readyToCountDown;
    private bool readyToStart;

    public float startingTime;

    // TODO: have default times for each
    private float lessThanMaxPlayerCountdown;
    private float atMaxPlayerCountdown;
    private float timeToStart;

    [SerializeField] private TMP_Text roomCode;
    [SerializeField] private TMP_Text peopleInRoom;

    private void Awake()
    {
        // If there is already an instance then destroy it and replace with the new one
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            Debug.LogWarning("Another instance of PhotonRoom found, there should only be one in the scene");
        }
        Instance = this;
        DontDestroyOnLoad(this);

        //pView = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void Start()
    {
        ResetTimer();

        photonPlayers = PhotonNetwork.PlayerList;
        numberOfPlayersInRoom = photonPlayers.Length;
        myNumberInRoom = numberOfPlayersInRoom;
        //Debug.Log(PhotonNetwork.NickName + " has joined the room " + PhotonNetwork.CurrentRoom.Name);

        if (MultiplayerSettings.Instance.delayedStart)
        {
            // TODO: Consider extracting to func
            Debug.Log("players in room: " + numberOfPlayersInRoom + "/" + MultiplayerSettings.Instance.maxPlayerCount);
            if (numberOfPlayersInRoom > 1)
            {
                readyToCountDown = true;
            }
            if (numberOfPlayersInRoom == MultiplayerSettings.Instance.maxPlayerCount)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        else
        {
            StartGame();
        }

        UpdateText();
    }

    void UpdateText()
    {
        roomCode.text = "Room code: " + PhotonNetwork.CurrentRoom.Name;
        peopleInRoom.text = PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    void Update()
    {
        if(MultiplayerSettings.Instance.delayedStart)
        {
            if(numberOfPlayersInRoom == 1)
            {
                ResetTimer();
            }
            if(!isGameLoaded)
            {
                if(readyToStart)
                {
                    atMaxPlayerCountdown -= Time.deltaTime;
                    lessThanMaxPlayerCountdown = atMaxPlayerCountdown;
                    timeToStart = atMaxPlayerCountdown;
                }
                else if(readyToCountDown)
                {
                    lessThanMaxPlayerCountdown -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayerCountdown;
                }
                if(timeToStart <= 0)
                {
                    StartGame();
                }
            }
        }
    }

    /// <summary>
    /// Runs when this user sucessfully joins a room
    /// </summary>
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        //SceneManager.LoadScene("Room");

        Debug.Log("PhotonRoom.OnJoinedRoom");

        UpdateText();
    }

    /// <summary>
    /// Runs when another user joins this room
    /// </summary>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + " has joined room");
        photonPlayers = PhotonNetwork.PlayerList;
        numberOfPlayersInRoom++;

        if(MultiplayerSettings.Instance.delayedStart)
        {
            Debug.Log("players in room: " + numberOfPlayersInRoom + "/" + MultiplayerSettings.Instance.maxPlayerCount);

            if(numberOfPlayersInRoom > 1)
            {
                readyToCountDown = true;
            }
            if (numberOfPlayersInRoom == MultiplayerSettings.Instance.maxPlayerCount)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }

        UpdateText();
    }

    /// <summary>
    /// Runs when another user leaves this room
    /// </summary>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left room");
        photonPlayers = PhotonNetwork.PlayerList;
        numberOfPlayersInRoom--;

        UpdateText();
    }

    void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        if(MultiplayerSettings.Instance.delayedStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiplayerSettings.Instance.multiplayerSceneIndex);
    }

    void ResetTimer()
    {
        readyToCountDown = false;
        readyToStart = false;
        lessThanMaxPlayerCountdown = startingTime;
        atMaxPlayerCountdown = 6;
        timeToStart = startingTime;
    }
    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentSceneIndex = scene.buildIndex;
        if(currentSceneIndex == MultiplayerSettings.Instance.multiplayerSceneIndex)
        {
            isGameLoaded = true;
        }

        /*if(MultiplayerSettings.Instance.delayedStart)
        {
            pView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
        }
        else
        {
            RPC_CreatePlayer();
        }*/
    }

    [PunRPC]
    void RPC_LoadedGameScene()
    {
        playersInGame++;
        if(playersInGame == PhotonNetwork.PlayerList.Length)
        {
            //pView.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    } 

    [PunRPC]
    void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), Vector3.zero, Quaternion.identity);
    }
}
