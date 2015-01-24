using UnityEngine;
using System.Collections;

public class OctopusConnector : PhotonBehaviour {
    public GameObject octopusPrefab;

	// Use this for initialization
	void Start () {

        if (!photonView.isMine)
        {
            Debug.Log("Connecting the player input to the correct tentacle");
            // connects input to the correct tentacle

            // TODO: here try to find existing octopuses to connect to

            // if no octopus exists, create one
            GameObject startPos = GameObject.Find("OctopusStartingPos");
            GameObject octopus = (GameObject)Instantiate(octopusPrefab, startPos.transform.position, Quaternion.identity);

            CubeMover mover = octopus.GetComponentInChildren<CubeMover>();
            mover.input = GetComponent<TouchInputReader>();
        }
	}
}
