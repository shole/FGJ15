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

            TouchInputReader touchInputReader = GetComponent<TouchInputReader>();

            bool octoFound = false;

            // TODO: here try to find existing octopuses to connect to
            GameObject[] octos = GameObject.FindGameObjectsWithTag("Octopus");
            for (int i = 0; i < octos.Length; i++)
            {
                CubeMover[] movers = octos[i].GetComponentsInChildren<CubeMover>();

                for (int j = 0; j < movers.Length; j++)
                {
                    // check if the tentacle is already controlled by someone
                    if (!movers[j].HasInput())
                    {
                        // un-controlled tentacle found
                        movers[j].SetInput(touchInputReader);
                        octoFound = true;
                        Debug.Log("Connecting the player to tentacle in existing octopus");
                        break;
                    }
                }
            }

            if (!octoFound)
            {
                Debug.Log("Connecting the player to a tentacle in a new octopus");
                // if no octopus exists, create one
                GameObject startPos = GameObject.Find("OctopusStartingPos");
                GameObject octopus = (GameObject)Instantiate(octopusPrefab, startPos.transform.position, Quaternion.identity);

                CubeMover mover = octopus.GetComponentInChildren<CubeMover>();
                mover.SetInput(touchInputReader);
            }
        }
	}
}
