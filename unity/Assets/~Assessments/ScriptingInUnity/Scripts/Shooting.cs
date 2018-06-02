using UnityEngine;

/// <summary>
/// The Shooting class contains all properties for shooting. This allows for
/// an object to be instanced and shot from a point in the world.
/// </summary>
public class Shooting : MonoBehaviour {

	[SerializeField] protected Transform head;						// Head component from which the spawn point is attatched
	[SerializeField] protected Transform spawnPoint;				// Position from which projectiles are spawned and shot
	[SerializeField] protected Transform fireBallPrefab;			// Fire ball to shoot
	[SerializeField] protected float fireBallVelocity = 2000.0f;    // How fast fire balls will travel

	[Header ("Effects")]
	[SerializeField] protected GameObject muzzleFlash;				// Muzzle flash to spawn when shooting
	[SerializeField] protected AudioSource effectsSound;			// Audio source for sound effect


	// Used to assign head and spawn point, if not assigned
	protected void FindMissingComponents () {
		// If no head was assigned, assign a head
		if (!head) {
			if (transform.Find ("Head")) {
				// If head exists as child of character, assign head to the child
				head = transform.Find ("Head").transform;
			} else {
				// If no head exists as child of character, assign character as head
				head = transform;
			}
		}
		
		// If no spawn point was assigned, assign a spawn point
		if (!spawnPoint) {
			// Look for an object parented to the head named "SpawnPoint"
			spawnPoint = head.transform.Find ("SpawnPoint").transform;
		}
	}

	// Used to shoot projectiles from a point
	protected void Shoot () {
		// If a spawn point is assigned, shoot at target!
		if (spawnPoint) {
			// If fire ball prefab was assigned, spawn a fire ball!
			if (fireBallPrefab) {
				Transform fireBall = Instantiate (fireBallPrefab,
					spawnPoint.position, head.rotation) as Transform;

				// Add force to our fire ball
				fireBall.GetComponent<Rigidbody> ().AddForce
					(head.transform.forward * fireBallVelocity);
			} else {
				// Log warning if no fire ball prefab was assigned
				Debug.LogWarning (gameObject.name + "'s Fire Ball Prefab not" +
					" assigned! " + gameObject.name + " Please assign a prefab to shoot.");
			}
			if (muzzleFlash) {
				// If muzzle flash was assigned, spawn muzzle flash as child of spawn point
				GameObject flash = Instantiate (muzzleFlash,
					spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
				flash.transform.parent = spawnPoint.gameObject.transform;
			}
			if (effectsSound) {
				// If sound effect was assigned, play sound effect
				effectsSound.PlayOneShot (effectsSound.clip);
			}
		} else {
			// Log warning if no spawn point was assigned
			Debug.LogWarning (gameObject.name + "'s Spawn Point not " +
				"assigned! Please assign a spawn point or create an empty object named “SpawnPoint” as a child of the head component.");
		}
	}
}
