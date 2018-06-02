using UnityEngine;

// Place the script in group in the component menu
[AddComponentMenu ("Player/Health")]

/// <summary>
/// The Player Health component controls mortality for user-controlled
/// characters. The Health Component allows for a number of lives, an array
/// tags to identify hazardous objects/surfaces, and a point from which to
/// spawn/respawn from. This component will also communicate to the Game
/// Manager to control the win state of the game.
/// </summary>
public class PlayerHealth : Health {

	// Use this for initialization
	void Start () {
		// Assign our Score Manager reference
		manager = FindObjectOfType<GameManager> ();

		// Assign opponent fire ball tag
		opponentFireBallTag = "EnemyFireBall";
	}

	// Update is called once per frame
	void Update () {
		if (dead) {
			// Subtract life and respawn
			DieAndRespawn ();

			if (lives == 0) {
				// If manager was assigned, trigger game lose state
				if (manager) {
					manager.Lose ();
				}
			}
		}
	}
}
