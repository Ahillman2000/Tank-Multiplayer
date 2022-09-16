using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby Instance { get; private set; }

    public TMP_InputField joinRoomInputField;
    //public TMP_Text joinRoomInputFieldText;

    public List<Button> buttons = new List<Button>();

    private void Awake()
    {
        // If there is already an instance then destroy it and replace with the new one
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
            Debug.LogError("Another instance of PhotonLobby found, there should only be one in the scene");
        }
        Instance = this;
    }

    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Runs when the user connects to server
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("user connected to photon master server");

        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    /// <summary>
    /// Creates a room with a random code
    /// </summary>
    public void CreateNewRoom()
    {
        const string glyphs = "abcdefghijklmnopqrstuvwxyz";
        int charAmount = 5;

        string roomCode;
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < charAmount; i++)
        {
            sb.Append(glyphs[Random.Range(0, glyphs.Length)]);
        }

        roomCode = sb.ToString();

        RoomOptions roomOptions = new RoomOptions
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = 5,
        };

        PhotonNetwork.CreateRoom("Room " + roomCode, roomOptions);
    }

    /// <summary>
    /// Runs when a room has been sucessfully created
    /// </summary>
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("new room created");
    }

    /// <summary>
    /// Runs when a room fails to be created
    /// </summary>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("creating new room failed, " + message);
        CreateNewRoom();
    }

    /// <summary>
    /// Join a random room
    /// </summary>
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    /// <summary>
    /// Runs when a user fails to join random room and creates a new room
    /// </summary>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("failed to join random room. " + message +  ". Creating new room");
        CreateNewRoom();
    }

    /// <summary>
    /// Joins a room with a specified room code
    /// </summary>
    public void JoinRoom()
    {
        if(joinRoomInputField.text.Length > 0)
            PhotonNetwork.JoinRoom(joinRoomInputField.text);
    }

    /// <summary>
    /// Runs when a user sucessfully joins a specified room
    /// </summary>
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("room joined");
    }

    /// <summary>
    /// Runs when a user fails to join a specified room
    /// </summary>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("failed to join room. " + message);
    }

    /// <summary>
    /// Leaves the current room
    /// </summary>
    public void OnCancel()
    {
        PhotonNetwork.LeaveRoom();
    }
}
