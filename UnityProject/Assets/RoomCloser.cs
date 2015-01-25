using UnityEngine;
using System.Collections;

public class RoomCloser : MonoBehaviour {

	void OnApplicationQuit()
    {
        Debug.Log("Cleaning up");
        if (!PhotonNetwork.connected)
        {
            return;
        }
        if (PhotonNetwork.room == null)
        {
            return;
        }

        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        
    }
}
