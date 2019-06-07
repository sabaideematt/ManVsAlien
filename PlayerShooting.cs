using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
	
	public GameObject bulletPrefab;        //bullet template
	public Transform bulletSpawn;          //bulletspawning position
	public float fireRate    = 0.00001f;   
	public float nextFire    = 0.0f;       // Time until next fire
	public int damagePerShot = 20;         // The damage inflicted by each bullet.



	AudioSource shootAudio;         // ref to shooting audio 

	// Use this for initialization
	void Start () {
//		shootableMask = LayerMask.GetMask ("Shootable");
//		gunLine       = GetComponent<LineRenderer>;
		shootAudio    = GetComponent<AudioSource> ();
	}
		

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)){
			Fire();
		}
	}

	void Fire(){
		if (Time.time > nextFire) {
			shootAudio.Play ();
			nextFire = Time.time + fireRate;
			GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;

			// Add velocity to the bullet
			bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 20;

			// Destroy the bullet after 4 seconds
			Destroy(bullet, 4.0f);
		}
	}
}
