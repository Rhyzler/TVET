using UnityEngine;

// Place the script in group in the component menu
[AddComponentMenu ("AI/Enemy/Shooting")]

/// <summary>
/// The Turret Shooting component controls the ability for the an AI-controlled
/// character to aim at a target and shoot at a specified interval.
/// </summary>
public class TurretShooting : Shooting {
	[Header ("Targeting")]
	public Transform lookAtTarget;		// Target that the head shoots at
	public float damp = 3.0f;			// Look damping amount

	[Header ("Shooting")]
	public float shootFrequency = 2.0f;	// Delay at which fire balls are shot (seconds)

	float shootTimer;					// Used to control shooting velocity

	// Use this for initialization
	void Start () {
		// Find missing components
		FindMissingComponents ();

		// Run shooting timer
		shootTimer = shootFrequency;

		// If no target was assigned, target the player by default
		if (!lookAtTarget) {
			// Use "Player" tag to identify player
			lookAtTarget = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}

	// Update is called once per frame
	void Update () {
		// If target was assigned, rotate turret's head to look at target and shoot
		if (lookAtTarget) {
			// Calculate how much to rotate from current position to the target's
			// position.
			Quaternion rotate = Quaternion.LookRotation (lookAtTarget.position
				- head.transform.position);

			// If head is assigned, rotate head to look at target
			if (head) {
				head.rotation = Quaternion.Slerp (head.transform.rotation, rotate,
					Time.deltaTime * damp);
			} else {
				// Log warning when no head was assigned
				Debug.LogWarning (gameObject.name + "'s Head not assigned! "
					+ gameObject.name + " cannot aim without a head!");
			}

			// Run our shooting timer
			shootTimer -= Time.deltaTime;

			// When timer expires, shoot at target!
			if (shootTimer < 0) {
				Shoot ();

				// Reset our shooting timer
				shootTimer = shootFrequency;
			}
		} else {
			// Log warning when no target was assigned
			Debug.LogWarning (gameObject.name + "'s Look At Target not assigned! "
				+ gameObject.name + " cannot aim without a Look At Target!");
		}
	}
}
