using UnityEngine;
using System.Collections;

public class PlayerDirectionWheelAnimate : MonoBehaviour {

    PlayerInput playerinput;

	// Use this for initialization
	void Start () {
        playerinput = transform.parent.parent.GetComponent<PlayerInput>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerinput.direction.x + playerinput.direction.y == 0f)
        {
            transform.localScale = Vector3.zero;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
        //transform.localRotation = Quaternion.LookRotation(new Vector3(playerinput.direction.x, 0, playerinput.direction.y).normalized);
        //transform.localRotation.SetLookRotation(new Vector3(playerinput.direction.x, 0, playerinput.direction.y).normalized,Vector3.up);
        transform.localRotation = Quaternion.AngleAxis( Mathf.Atan2( playerinput.direction.x , playerinput.direction.y ) *60 , Vector3.forward );
        //transform.localRotation= Quaternion.
        //transform.localRotation = transform.localRotation.AngleAxis( playerinput.direction.x,Vector3.forward);
	}
}
