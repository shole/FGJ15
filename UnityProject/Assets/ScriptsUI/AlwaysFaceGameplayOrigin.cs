using UnityEngine;
using System.Collections;

public class AlwaysFaceGameplayOrigin : MonoBehaviour
{

    Transform GameplayOrigin;

	// Use this for initialization
	void Start () {
        GameplayOrigin = GameObject.Find("GameplayOrigin").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(GameplayOrigin.position);
	}
}
