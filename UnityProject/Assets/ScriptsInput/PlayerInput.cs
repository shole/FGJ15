using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public bool playeractive = false;

    public int playerid;
    public string playername;
    public Vector2 direction;
    public bool grabbing = false;
    public bool reach = false;
    public float lastreach=0;

    public float punchForce = 1f;
    public float reachForce = 1f;

    Transform reachTarget;
    Transform punchTarget;

    Transform Search(this Transform target, string name)
    {
        if (target.name == name) return target;

        for (int i = 0; i < target.childCount; ++i)
        {
            var result = Search(target.GetChild(i), name);

            if (result != null) return result;
        }

        return null;
    }

	// Use this for initialization
	void Start () {
        playeractive = true;
        playerid = 1;
        //reachTarget = transform.Find("ReachTarget");
        //punchTarget = transform.Find("PunchTarget");
        reachTarget = Search(transform,"ReachTarget");
        punchTarget = Search(transform,"PunchTarget");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        //GamePad.GetAxis(GamePad.Axis.Left, 1);
        if (direction.x + direction.y > 0.0f)
        {
            rigidbody.AddForce((punchTarget.position - transform.position).normalized * reachForce, ForceMode.Force);
        }
        if (Input.GetButtonDown("punch")) {
            Debug.Log("punch");
            rigidbody.AddForce((punchTarget.position - transform.position).normalized * punchForce, ForceMode.Impulse);
        }

	}
}
