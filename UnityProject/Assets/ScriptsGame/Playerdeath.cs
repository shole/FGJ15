using UnityEngine;
using System.Collections;

public class Playerdeath : MonoBehaviour {

    public float deathaccelerator=1.1f;
    Transform octopistartposition;

	// Use this for initialization
	void Start () {
        octopistartposition = transform.parent.parent.FindChild("OctopusStartingPos");
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
    void OnTriggerEnter(Collider hit)
    {
        GameObject ob = hit.gameObject;
        if (hit.tag == "Player")
        {
            Debug.Log("Shit hit the fan " + hit.name);
            // if - add check for dead player here and murder prefab if dead!
            ob.GetComponent<HumiliationElevator>().HumiliatePlayer();
            /*
            Rigidbody[] everybody = ob.transform.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < everybody.Length; i++)
            {
                everybody[i].Sleep();
                everybody[i].isKinematic = true;

                everybody[i].transform.position = octopistartposition.position;// +(everybody[i].transform.position - ob.transform.parent.position);

                everybody[i].isKinematic = false;

                everybody[i].velocity = Vector3.zero;
                everybody[i].angularVelocity = Vector3.zero;

                everybody[i].mass *= deathaccelerator;
                everybody[i].WakeUp();
            }

            ob.transform.parent.position = octopistartposition.position;

            ob.transform.rigidbody.Sleep();

            ob.transform.rigidbody.isKinematic = true;

            ob.transform.localScale = hit.transform.localScale * deathaccelerator;

            ob.transform.rigidbody.isKinematic = false;
            ob.transform.rigidbody.velocity = Vector3.zero;
            ob.transform.rigidbody.angularVelocity = Vector3.zero;
            ob.transform.rigidbody.WakeUp();

            // rigidbody mass
            // strength
            
            // set all rigidbody.velocity=Vector3.zero;
            */
        }
    }
}
