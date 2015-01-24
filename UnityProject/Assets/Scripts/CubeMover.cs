using UnityEngine;
using System.Collections;

public class CubeMover : MonoBehaviour {
    private TouchInputReader input;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (input == null)
        {
            input = FindObjectOfType<TouchInputReader>();
        }
        if (input == null)
        {
            Debug.Log("couldn't find input");
            return;
        }

        Debug.Log("translating.. " + input.lastY + ", " + input.lastX);
        transform.Translate(input.lastX / 100f, 0, input.lastY/100f);
	}
}
