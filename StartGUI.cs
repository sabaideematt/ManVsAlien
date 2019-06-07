using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGUI : MonoBehaviour {
	Transform player;               // Reference to Player pos
	Transform enemy;                // Reference to Enemy Spawns pos
	PlayerMobility playerMobility;  // Reference to the player mob class
	PlayerShooting playerShooting;  // Reference to the player shoot class
	EnemyManager   enemies;         // Reference to enemy man class
	Button playButton;              // Reference to the play button on Start Menu 
	GameObject startMenu;
	AudioSource Welcome;

	// Use this for initialization
	void Awake () {
		player         = GameObject.FindGameObjectWithTag ("Player").transform;
		playerMobility = player.GetComponent<PlayerMobility> ();
		playerShooting = player.GetComponent<PlayerShooting> ();
		enemy          = GameObject.FindGameObjectWithTag ("EnemyManager").transform;
		enemies        = enemy.GetComponent<EnemyManager> ();
		playButton     = GetComponent<Button> ();
		startMenu      = GameObject.FindGameObjectWithTag ("StartMenu");
		AudioSource[] allAudio = player.GetComponents<AudioSource> ();
		Welcome        = allAudio [2];

		playerMobility.enabled = false;
		playerShooting.enabled = false;
		enemies.enabled        = false;

		playButton.onClick.AddListener(TaskOnClick);

	}


	void TaskOnClick () {
		Welcome.Play ();
		startMenu.SetActive (false); 
		playerMobility.enabled = true;
		playerShooting.enabled = true;
		enemies.enabled        = true;
	}
}
