using UnityEngine;
using System.Collections;

public class AlwaysFaceGameplayOrigin : MonoBehaviour
{

    Transform GameplayOrigin;

    Transform parentbody;

	// Use this for initialization
	void Start () {
        GameplayOrigin = GameObject.Find("GameplayOrigin").transform;
        parentbody = transform.parent;
        while (parentbody.gameObject.layer == LayerMask.NameToLayer("Tentacle"))
        {
            parentbody = parentbody.parent;
        }
        parentbody=parentbody.FindChild("Armature|Base");
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 pos = transform.position;
        //transform.rotation = Quaternion.LookRotation((GameplayOrigin.position - transform.position).normalized);
        transform.rotation = Quaternion.LookRotation((GameplayOrigin.position - parentbody.position).normalized);
        
        transform.rotation.SetAxisAngle(Vector3.right, 22f);
        //transform.LookAt(GameplayOrigin.position);
	}
}
