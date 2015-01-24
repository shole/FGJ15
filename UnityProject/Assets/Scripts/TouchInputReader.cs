using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchInputReader : PhotonBehaviour {
    
	// Use this for initialization
	void Start () {
        screenCenter.Set(Screen.width, Screen.height);
        screenCenter /= 2f;
        FindObjectOfType<Text>().text = "starting..";
	}

    public float lastX, lastY;
    public bool unhandledDoubleTap = false;
    private Vector2 screenCenter = new Vector2();

    private float doubleTapCooldownSecs = 0.5f;
    private float doubleTapCDCounter = 0;
	
	// Update is called once per frame
	void Update () {
        if (!photonView.isMine)
        {
            return;
        }

        doubleTapCDCounter += Time.deltaTime;

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            
            if (t.tapCount > 1)
            {
                if (doubleTapCDCounter > doubleTapCooldownSecs)
                {
                    RPC(DoubleTap, PhotonTargets.MasterClient);
                    doubleTapCDCounter = 0;
                }
            }
            else
            {
                // just use as moving
                lastX = (t.position.x - screenCenter.x) / screenCenter.x;
                lastY = (t.position.y - screenCenter.y) / screenCenter.y;

                Debug.Log("read " + CoordsToString());
                FindObjectOfType<Text>().text = CoordsToString();
            }
        }
        else
        {
            lastX = 0;
            lastY = 0;
        }
	}

    [RPC]
    public void DoubleTap()
    {
        Debug.Log("doubletapped!");
        unhandledDoubleTap = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(lastX);
            stream.SendNext(lastY);
            // Debug.Log("sent " + CoordsToString());
        }
        else
        {
            lastX = (float)stream.ReceiveNext();
            lastY = (float)stream.ReceiveNext();
            // Debug.Log("received" + CoordsToString());
        }
    }

    private string CoordsToString()
    {
        return lastX.ToString("0.000") + ", " + lastY.ToString("0.000");
    }
}
