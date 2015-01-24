using UnityEngine;
using System.Collections;

public class NetworkStartServer : MonoBehaviour {
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public const string roomName = "FJG_TEST";

    void OnReceivedRoomListUpdate()
    {
        Debug.Log("Created new game and joined it as master client");
        PhotonNetwork.CreateRoom(roomName);
    }
    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room");
    }
}
