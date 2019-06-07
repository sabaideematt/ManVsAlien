using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	public GameObject gameOver;                     
	Rigidbody2D playerBody;

	AudioSource playerAudio;                             // Reference to the AudioSource component.
	PlayerMobility playerMobility;                              // Reference to the player's movement.
	PlayerShooting playerShooting;
	public bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.


	void Start ()
	{
		// Setting up the references.
		AudioSource[] allAudio = GetComponents <AudioSource> ();
		playerAudio = allAudio [1];
		playerMobility = GetComponent <PlayerMobility> ();
		playerShooting = GetComponent <PlayerShooting> ();
		currentHealth = startingHealth;
		playerBody = GetComponent<Rigidbody2D> ();
	}


	void Update ()
	{
		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
			playerAudio.Play ();
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}


	public void TakeDamage (int amount)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;

		// Play the hurt sound effect.
		//playerAudio.Play ();

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}


	void Death ()
	{
		// Set the death flag so this function won't be called again.
		isDead                 = true;
		playerShooting.enabled = false;
		playerMobility.enabled = false;
		gameOver.SetActive (true);
		playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
	}       
}
