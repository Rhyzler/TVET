using UnityEngine;
using UnityEngine.Events;

// Place the script in the Utilities group in the component menu
[AddComponentMenu ("Utilities/Game Manager")]

/// <summary>
/// The Game Manager controls the rules of the game. The Game manager keeps
/// track of how many enemies exist in the scene. Once all enemies have been
/// defeated, player wins the game. If the player dies before all the enemies
/// have been defeated, the player loses the game. The Game Manager also
/// controls the events to occur prior to the player winning or losing the
/// game.
/// </summary>
public class GameManager : MonoBehaviour {

	[HideInInspector] public int enemyCount;	// Count for how many enemies remain in the scene
												// When the player defeats all enemies, the player wins!
	[SerializeField] UnityEvent onWin;			// Events to occur when the player wins
	[SerializeField] UnityEvent onLose;			// Events to occur when the player loses

	// Use this to update the amount of enemies remaining in the scene
	public void UpdateEnemyCount (int count) {
		enemyCount += count;
		
		// When there are no enemies reminaing, we win the game!
		if (enemyCount <= 0) {
			Win ();
		}
	}

	// Used to set the amount of enemies remaining in the scene. Useful for
	// resetting game state
	public void SetEnemyCount (int count) {
		enemyCount = count;
	}
	
	// Used to win the game
	public void Win () {
		// Trigger events to occur upon winning
		onWin.Invoke ();
	}

	// Used to lose the game
	public void Lose () {
		// Trigger events to occur upon losing
		onLose.Invoke ();
	}
}
