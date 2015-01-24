using UnityEngine;
using System.Collections;

public class CubeMover : MonoBehaviour {
    public TouchInputReader input;
    public float moveForceModifier = 15f;
    public float pullForceModifier = 100f;
	
	// Update is called once per frame
	void Update () {
        if (input.lastX != 0 || input.lastY != 0)
        {
            if (rigidbody.isKinematic)
            {
                // grabbing is on, and user is giving input, so cause a force
                var list = this.GetComponentsInParent<Rigidbody>();

                Rigidbody endObject = null;
                Rigidbody beginObject = null;

                var i = 0;

                foreach (Rigidbody item in list)
                {
                    // Split (in armature the current tentacle hierarchy has bones splitted by '|'
                    // Ugly hax
                    var nameParts = item.name.Split('|');

                    if (nameParts.Length >= 2)
                    {
                        if (i == 0)
                        {
                            endObject = item; // first bone
                        }
                        else
                        {
                            beginObject = item; // last bone
                        }

                        i++;
                    }
                }

                // Points hopefully defined! APPLY FORCE
                if (endObject != null && beginObject != null)
                {
                    Vector3 direction = endObject.position - beginObject.position;

                    beginObject.AddForce(direction * pullForceModifier);
                }
            }
            else
            {
                // just move the tentacle
                rigidbody.AddForce(input.lastX * moveForceModifier, 0, input.lastY * moveForceModifier);
            }
        }
        if (input.unhandledDoubleTap)
        {
            input.unhandledDoubleTap = false;
            rigidbody.isKinematic = !rigidbody.isKinematic;
        }
	}
}
