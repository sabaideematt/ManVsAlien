using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
	public PlayerHealth playerHealth;       // Reference to the player's heatlh.
	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 2f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public float ChangeSpawntimer;          // Tells us when to change the spawnTime
	public int changeSpawnTime;
	public int EnemyCount;
	public GameObject enemyNuke;
	AudioSource NukeSound;
	public static bool isNuked;
	public Image nukeImage;  
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 1f, 1f, 1f);       // The colour the damageImage is set to, to flash.
	public int nukeSpawn;
	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating("Spawn", 1f, spawnTime);
		ChangeSpawntimer   = 10f;
		changeSpawnTime    = 1;
		EnemyCount = 0;
		NukeSound = GetComponent<AudioSource> ();
		nukeSpawn = 30;
	}


	void Spawn ()
	{
		EnemyCount++;
		// If the player has no health left...
		if(playerHealth.currentHealth <= 0f)
		{
			// ... exit the function.
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		if (EnemyCount % nukeSpawn == 0) {
			Instantiate (enemyNuke, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
			nukeSpawn *= 3;
		} else {
			Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		}
	}

	void Update (){
		ChangeSpawntimer -= Time.deltaTime;

		if (ChangeSpawntimer <= 0 && spawnTime > 0.5f) {
			CancelInvoke ();
			spawnTime -= 0.5f;
			changeSpawnTime++;
			ChangeSpawntimer = 10f * changeSpawnTime;
			InvokeRepeating ("Spawn", 0f, spawnTime);
		} 

		if (spawnTime == 1.0f) {
			LastInvoke ();
		}

		if (isNuked) {
			NukeSound.Play ();
			nukeImage.color = flashColour;
			isNuked = false;
		} else {
			nukeImage.color = Color.Lerp (nukeImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
	}

	void LastInvoke(){
		CancelInvoke ();
		spawnTime -= 0.5f;
		changeSpawnTime++;
		InvokeRepeating ("Spawn", 0f, spawnTime);
	}
}