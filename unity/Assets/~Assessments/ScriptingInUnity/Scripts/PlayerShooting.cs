using UnityEngine;

// Place the script in group in the component menu
[AddComponentMenu ("Player/Shooting")]

/// <summary>
/// Player shooting controls the player character's ability to shoot. Shooting
/// is controlled by user input.
/// </summary>
public class PlayerShooting : Shooting {
	[Header ("Input")]
	[SerializeField] string fireInput = "Fire1";	// Used to identify input axis to use for shooting

	void Start () {
		// Find missing components
		FindMissingComponents ();
	}

	// Update is called once per frame
	void Update () {
		// When space bar is pressed, shoot!
		if (Input.GetButtonDown (fireInput)) {
			Shoot ();
		}
	}
}
