using UnityEngine;
using System.Collections;

public class GrabHandler : MonoBehaviour {
    TouchInputReader inputHandler;
	
	// Update is called once per frame
	void Update () {
        if (inputHandler == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("InputHandler");
            if (go == null) {
                return;
            }
            inputHandler = go.GetComponent<TouchInputReader>();
        }
        if (inputHandler == null)
        {
            return;
        }
	}

    public void Grab()
    {
        // Debug.Log("grabbing");
        //inputHandler.DoDoubleTap();
    }
}
