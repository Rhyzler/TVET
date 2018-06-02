using UnityEngine;

// Place the script in the Utilities group in the component menu
[AddComponentMenu ("Utilities/Fire Ball Collision")]

/// <summary>
/// The Fire Ball Collision component destroys the fire ball when it collides
/// with an object.
/// </summary>
public class FireBallCollision : MonoBehaviour {

	[SerializeField] string [] ignoreTags;	// Tags to ignore when colliding

	[Header ("Effects")]
	[SerializeField] GameObject explosion;	// Explosion to spawn when fire ball has hit something

	void OnTriggerEnter (Collider hit) {
		// Check if hit object isn't tagged with tag to ignore
		foreach (string tag in ignoreTags) {
			if (hit.tag != tag) {
				// Explode fire ball
				Explode ();
			}
		}

		// Check for untagged objects
		if (hit.tag == "Untagged") {
			// Explode fire ball
			Explode ();
		}
	}

	// Used to explode fire ball
	void Explode () {
		if (explosion) {
			// If explosion was assigned, spawn explosion
			Instantiate (explosion, transform.position, transform.rotation);
		}

		// Destory fire ball
		Destroy (gameObject);
	}
}
