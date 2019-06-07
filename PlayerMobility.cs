using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobility : MonoBehaviour {
	Rigidbody2D player;                 //ref to player component 
	Vector2 initial;                    //Initial position of player

	void Start() {
		player     = GetComponent<Rigidbody2D> ();
		initial    = player.transform.position;
	}

	void FixedUpdate(){
		//This section is for the rotation
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);
		GetComponent<Rigidbody2D> ().angularVelocity = 0;

		//This section is for the player's x,y movement
		if (((Input.GetKey (KeyCode.RightArrow))|| (Input.GetKey (KeyCode.D))) && player.transform.position.x < 7.66 ) {
			initial.x = initial.x + 0.1f;
		}
		if (((Input.GetKey (KeyCode.LeftArrow)) || (Input.GetKey (KeyCode.A))) && player.transform.position.x > -7.3) {
			initial.x = initial.x - 0.1f;
		}
		if (((Input.GetKey (KeyCode.UpArrow))   || (Input.GetKey (KeyCode.W))) && player.transform.position.y < 5.5) {
			initial.y = initial.y + 0.1f;
		}
		if (((Input.GetKey (KeyCode.DownArrow)) || (Input.GetKey (KeyCode.S))) && player.transform.position.y > -5.24) {
			initial.y = initial.y - 0.1f;
		}
		if ((Input.GetKey (KeyCode.Escape))) {
			Application.Quit();
		}
		player.MovePosition(initial);
	}
}
