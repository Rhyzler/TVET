using UnityEngine;

// Place the script in group in the component menu
[AddComponentMenu ("AI/Enemy/Health")]

/// <summary>
/// The Turret Health component controls mortality for AI-controlled
/// characters. The Health Component allows for a number of lives, an array
/// tags to identify hazardous objects/surfaces, and a point from which to
/// spawn/respawn from. This component will also communicate to the Game
/// Manager to control the win state of the game.
/// </summary>
public class TurretHealth : Health {

	// Use this for initialization
	void Start () {
		// Assign our Score Manager reference
		manager = FindObjectOfType<GameManager> ();

		// If manager was assigned, add this enemy to the enemy count
		if (manager) {
			manager.UpdateEnemyCount (1);
		}

		// Assign opponent fire ball tag
		opponentFireBallTag = "PlayerFireBall";
	}

	// Update is called once per frame
	void Update () {
		if (dead) {
			// Subtract life and respawn
			DieAndRespawn ();

			if (lives == 0) {
				// If manager was assigned, subtract from enemy count
				if (manager) {
					manager.UpdateEnemyCount (-1);
				}

				// Character has run out of lives, destroy character
				Destroy (gameObject);
			}
		}
	}
}
