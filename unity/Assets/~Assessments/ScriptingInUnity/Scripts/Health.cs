using UnityEngine;

/// <summary>
/// The Health component controls mortality for characters. The Health
/// Component allows for a number of lives, an array tags to identify hazardous
/// objects/surfaces, and a point from which to spawn/respawn from. This
/// component will also communicate to the Game Manager to control the win or
/// lose state of the game.
/// </summary>
public class Health : MonoBehaviour {
	public int lives = 3;									// Amount of lives character has
	[SerializeField] string [] hazardTags
		= new string [] { "Fallout" };						// Used to determine which objects upon collision will kill character
	[SerializeField] protected Transform spawnPoint;		// Position at which character spawns from

	[Header("Effects")]
	[SerializeField] protected GameObject explosion;		// Explosion to spawn when hit
	[SerializeField] protected AudioSource effectsSound;    // Audio source for sound effect
	[SerializeField] protected AudioClip dieClip;			// Audio clip to play when character dies

	protected bool dead = false;							// State for whether or not character has died
	protected string opponentFireBallTag;					// Tag used to identify fire ball from opponent
	protected GameManager manager;							// Reference to Game Manager. This needs to be referenced in order for
															// the player to lose the game once the character has run out of lives
	
	// Controls events to occur opon character's death
	protected void DieAndRespawn () {
		// Subtract a life
		lives -= 1;

		// If spawn point was assigned, move character to spawn point position
		if (spawnPoint) {
			transform.position = spawnPoint.position;
		}

		// Character is no longer dead
		dead = false;
	}

	// OnTriggerEnter is called when the Collider other enters the trigger
	void OnTriggerEnter (Collider hit) {
		// If character collided with any defined hazards, the character dies
		foreach (string tag in hazardTags) {
			if (hit.gameObject.tag == tag && lives > 0) {
				dead = true;

				// Explosion effect
				if (explosion) {
					// If explosion was assigned, spawn explosion as child of head
					GameObject childExplosion = Instantiate (explosion,
						transform.position, transform.rotation) as GameObject;
					childExplosion.transform.parent = gameObject.transform;
				}

				// Sound effect
				if (effectsSound && dieClip) {
					// Play die sound effect
					effectsSound.PlayOneShot (dieClip);
				}
			}
		}

		// Destroy fire ball from opponent to prevent it from travelling through character
		if (hit.tag == opponentFireBallTag) {
			Destroy (hit.gameObject);
		}
	}
}
