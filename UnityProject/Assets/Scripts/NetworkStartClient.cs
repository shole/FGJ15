using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkStartClient : MonoBehaviour {
    public GameObject inputPrefab;
    public Text statusText;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
    
    private RoomInfo[] roomsList;

    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();

        foreach (RoomInfo info in roomsList)
        {
            if (info.name.Equals(NetworkStartServer.roomName))
            {
                PhotonNetwork.JoinRoom(info.name);
                Debug.Log("Joined existing game as client");
                return;
            }
        }
        Debug.Log("Couldn't find an existing room from the server");
        statusText.text = "Couldn't find existing game";
    }
    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room");
        PhotonNetwork.Instantiate(inputPrefab.name, Vector3.zero,
                Quaternion.identity, 0, new object[] { PhotonNetwork.player.ID });
        statusText.text = "connected";

    }
}
