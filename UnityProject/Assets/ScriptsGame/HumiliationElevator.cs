using UnityEngine;
using System.Collections;

public class HumiliationElevator : MonoBehaviour
{

    public float elevatorSpeed = 0.1f;
    public float liftHeight = 10f;
    public bool isBeingHumiliated = true;
    public void HumiliatePlayer()
    {
        isBeingHumiliated = true;
        rigidbody.isKinematic = true;
        state = 0;
    }
    int state = 0;
    Transform droppoint;

    // Use this for initialization
    void Start()
    {
        droppoint = GameObject.Find("OctopusStartingPos").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBeingHumiliated)
        {
            if (state == 0)
            {
                if (transform.position.y <= liftHeight)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, droppoint.position.y + liftHeight, transform.position.z), elevatorSpeed);
                }
                else
                {
                    state = 1;
                    CubeMover[] players = transform.parent.GetComponentsInChildren<CubeMover>();
                    int playersPresent = 0;
                    for (int i=0; i < players.Length; i++)
                    {
                        if (players[i].prevPlayerID >= 0)
                        {
                            playersPresent++;
                        }
                    }
                    if (playersPresent == 0)
                    {
                        Destroy(transform.parent.parent.gameObject);
                    }
                }
            }
            if (state == 1)
            {
                if (Vector3.Distance(transform.position, droppoint.position) > 0.5f)
                {
                    transform.position = Vector3.Lerp(transform.position, droppoint.position, elevatorSpeed);
                }
                else
                {
                    isBeingHumiliated = false;
                    rigidbody.isKinematic = false;
                    state = 0;
                }
            }
        }
    }
}
