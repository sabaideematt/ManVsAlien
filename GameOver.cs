using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour {
	GameObject[] allEnemies;   // Reference to enemies on board 
	GameObject[] allNukes;
	Button restartButton;      // Reference to restart button  
	GameObject player;
	PlayerMobility playerMobility;
	PlayerShooting playerShooting;
	PlayerHealth playerHealth;
	Rigidbody2D playerBody;
	public GameObject gameOver;
	GameObject enemyManagerObj;
	EnemyManager enemyManager;
	public Slider healthSlider;

	// Use this for initialization
	void Start () {
//		allEnemies    = GameObject.FindGameObjectsWithTag ("Enemy"); 
		restartButton = GetComponent<Button> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerMobility = player.GetComponent<PlayerMobility> ();
		playerShooting = player.GetComponent<PlayerShooting> ();
		playerHealth   = player.GetComponent<PlayerHealth> ();
		playerBody     = player.GetComponent<Rigidbody2D> ();
		enemyManagerObj = GameObject.FindGameObjectWithTag ("EnemyManager");
		enemyManager    = enemyManagerObj.GetComponent<EnemyManager> ();

		restartButton.onClick.AddListener(TaskOnClick);

	}

	void DestroyEnemies(){
		allEnemies    = GameObject.FindGameObjectsWithTag ("Enemy");
		allNukes      = GameObject.FindGameObjectsWithTag ("EnemyNuke");
		for (int i = 0; i < allEnemies.Length; i++) {
			Destroy (allEnemies[i]);
		}
		for (int i = 0; i < allNukes.Length; i++) {
			Destroy (allNukes[i]);
		}
	}


	void TaskOnClick(){
		DestroyEnemies ();
		playerHealth.currentHealth  = 100;
		healthSlider.value = 100;
		playerHealth.isDead = false;
		playerMobility.enabled = true;
		playerShooting.enabled = true;
		playerBody.constraints = RigidbodyConstraints2D.None;
		enemyManager.changeSpawnTime    = 1;
		enemyManager.spawnTime          = 2f;
		enemyManager.EnemyCount = 0;
		enemyManager.nukeSpawn = 30;
		ScoreManager.score = 0;
		gameOver.SetActive (false);
	}
}
