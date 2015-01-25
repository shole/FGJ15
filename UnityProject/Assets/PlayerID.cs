using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerID : PhotonBehaviour {
    Text text;
    

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.player == null)
        {
            return;
        }
        if (PhotonNetwork.player.ID == null)
        {
            Debug.Log("player id null");
            return;
        }
        text.text = PhotonNetwork.player.ID.ToString();
	}
}
