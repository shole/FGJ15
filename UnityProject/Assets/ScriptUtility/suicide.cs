using UnityEngine;
using System.Collections;

public class suicide : MonoBehaviour {

    public float timeToDie = 10f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToDie);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
