using UnityEngine;
using System.Collections;

public class Grab : MonoBehaviour {

	// Use this for initialization
	void Start () {
		HingeJoint hg = GetComponent<HingeJoint> ();
		// GetComponentsInChildren<Rigidbody>
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("space")) {
			rigidbody.isKinematic = !rigidbody.isKinematic;
			Debug.Log("space pressed");
		}

		if (Input.GetKey ("a")) {
			//var list = this.GetComponentsInChildren<Transform>();
			var list = this.GetComponentsInParent<Transform>();
			foreach(Transform item in list) {
				// Split
				var nameParts = item.name.Split('|');

				if(nameParts.Length >= 2) {
					Debug.Log("rescale"+nameParts[1]);
					//item.localScale = new Vector3(1.1f,1.1f,1.1f);
					item.localScale = new Vector3(1.0f,0.5f,1.0f);
					rigidbody.isKinematic = true;
				}

				Debug.Log("item"+nameParts[0]);

			}
		}

	}
}
