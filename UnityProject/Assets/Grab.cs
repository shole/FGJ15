using UnityEngine;
using System.Collections;

public class Grab : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// HingeJoint hg = GetComponent<HingeJoint> ();
		// GetComponentsInChildren<Rigidbody>
	}
	
	void Pull() {
		// Get All Transforms
		var list = this.GetComponentsInParent<Rigidbody>();
		
		Rigidbody endObject = null;
		Rigidbody beginObject = null;
		
		var i = 0;
		
		foreach(Rigidbody item in list) {
			// Split (in armature the current tentacle hierarchy has bones splitted by '|'
			// Ugly hax
			var nameParts = item.name.Split('|');
			
			if(nameParts.Length >= 2) {
				if(i == 0) {
					endObject = item; // first bone
				} else {
					beginObject = item; // last bone
				}
				
				i++;
			}
		}
		
		// Points hopefully defined! APPLY FORCE
		if(endObject != null && beginObject != null) {
			Vector3 direction = endObject.position - beginObject.position;
			
			beginObject.AddForce(direction * 100);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("space")) {
			rigidbody.isKinematic = !rigidbody.isKinematic;
			Debug.Log("space pressed");
		}
		
		if (Input.GetKey ("a")) {
			this.Pull();
		}
		
	}
}
