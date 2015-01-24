using UnityEngine;
using System.Collections;

public class CubeMover : MonoBehaviour {
    public TouchInputReader input;
    public float forceModifier = 15f;
	
	// Update is called once per frame
	void Update () {
        if (input.lastX != 0 || input.lastY != 0)
        {
            rigidbody.AddForce(input.lastX * forceModifier, 0, input.lastY * forceModifier);
        }
        if (input.unhandledDoubleTap)
        {
            input.unhandledDoubleTap = false;
            rigidbody.isKinematic = !rigidbody.isKinematic;
        }
	}
}
