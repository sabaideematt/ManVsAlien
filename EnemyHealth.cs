using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	bool isDead;                                // Whether the enemy is dead.
	EnemyMovement enemyMovement;                // Reference to enemy movement object
	Transform player;                           // reference to player pos
	PlayerShooting playerShoot;                 // Reference to Player shooting
	GameObject[] allEnemies;   // Reference to enemies on board 
	GameObject[] allNukes;
	int nukeScore;
	void Start ()
	{
		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerShoot = player.GetComponent<PlayerShooting> ();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Bullet") {
			TakeDamage (playerShoot.damagePerShot);
			Destroy (coll.gameObject);
		}

	}

	public void TakeDamage (int amount)
	{
		// If the enemy is dead...
		if (isDead) {
			return;
		}

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;

		if(currentHealth <= 0)
		{
			isDead = true;
		}
	}

	void DestroyEnemies(){
		allEnemies    = GameObject.FindGameObjectsWithTag ("Enemy");
		allNukes      = GameObject.FindGameObjectsWithTag ("EnemyNuke");
		nukeScore     = allEnemies.Length + allNukes.Length;
		for (int i = 0; i < allEnemies.Length; i++) {
			Destroy (allEnemies[i]);
		}
		for (int i = 0; i < allNukes.Length; i++) {
			Destroy (allNukes[i]);
		}
	}

	void Update ()
	{
		// If the player has just been damaged...
		if(isDead)
		{
			if (gameObject.tag == "EnemyNuke") {
				DestroyEnemies ();
				ScoreManager.score += nukeScore;
				EnemyManager.isNuked = true;
			} else {
				ScoreManager.score += 1;        // To update the score if enemy dies 
				Destroy (gameObject);
			}
		}
	}
}
