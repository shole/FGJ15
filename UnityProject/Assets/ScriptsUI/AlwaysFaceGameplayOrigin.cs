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
        Vector3 directionvector = GameplayOrigin.position - parentbody.position;
        directionvector.y = 0f;
        transform.rotation = Quaternion.LookRotation(directionvector);
        transform.Rotate(Vector3.right, 77f);
        
        //transform.rotation.SetAxisAngle(Vector3.right, 22f);
        //Vector3 angles = transform.rotation.eulerAngles;
        //transform.rotation.SetEulerAngles(angles.x, 22f, angles.z);
        
        //transform.LookAt(GameplayOrigin.position);
	}
}
