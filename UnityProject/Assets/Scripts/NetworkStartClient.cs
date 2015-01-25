using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkStartClient : MonoBehaviour {
    public GameObject inputPrefab;
    public Text statusText;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        // GUI.Button(new Rect(100, 550, 250, 100), "asdf");
    }
    
    private RoomInfo[] roomsList;

    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();

        Debug.Log("Found rooms: " + roomsList.Length);

        if (roomsList.Length == 0)
        {
            statusText.text = "Couldn't find any existing games";
            return;
        }

        statusText.text = "";

        
    }

    void OnGUI()
    {
        if (!PhotonNetwork.connected)
        {
            return;
        }
        if (PhotonNetwork.room != null)
        {
            return;
        }
        if (roomsList == null)
        {
            return;
        }

        for (int i = 0; i < roomsList.Length; i++)
        {
            float width = Screen.width * 0.75f;
            float height = Screen.height * 0.1f;
            if (GUI.Button(new Rect(100, height + (height * 1.2f * i), width, height), "Join " + roomsList[i].name))
            {
                PhotonNetwork.JoinRoom(roomsList[i].name);
            }
        }
    }

    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room");
        PhotonNetwork.Instantiate(inputPrefab.name, Vector3.zero,
                Quaternion.identity, 0, new object[] { PhotonNetwork.player.ID });

        statusText.text = "connected";
    }

    void OnMasterClientSwitched()
    {
        Application.Quit();
    }
}
