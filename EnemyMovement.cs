using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	Transform player;                      // Reference to the player's position.
	PlayerHealth playerHealth;             // Reference to the player's health.
	public float speed = 3f;               // Enemy speed
	Vector3 dir;                           // Direction enemy is facing
	float angle;                           // Angle of rotation of enemy

	void Start ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
	}

	void Update ()
	{
		if(playerHealth.currentHealth > 0)
		{
		    transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
			dir = player.position - transform.position;
			angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		// Otherwise do nothing 
	} 
}