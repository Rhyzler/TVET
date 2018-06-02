using UnityEngine;
using UnityEngine.UI;

// Place the script in the UI in the component menu
[AddComponentMenu ("UI/Game Statistic Display")]

/// <summary>
/// The Game Statistics component controls the display of the player character's
/// health and enemy count statistics on the HUD.
/// </summary>
public class GameStats : MonoBehaviour {

	PlayerHealth health;											// Reference to Player Health component. We use properties from this component to display on the HUD
	GameManager manager;											// Reference to Game Manager component. We use properties from this component to display on the HUD
	
	[SerializeField] Text healthDisplay;							// UI text element to display amount of lives remaining
	[SerializeField] string healthLabel = "Lives: ";				// Label to display before lives remaining
	[SerializeField] Text enemyCountDisplay;						// UI text element to display amount of enemies remaining
	[SerializeField] string enemyCountLabel = "Enemies Remain: ";	// Label to display before enemy count

	// Awake is called when the script instance is being loaded
	void Awake () {
		// Assign component references
		health = FindObjectOfType<PlayerHealth> ();
		manager = FindObjectOfType<GameManager> ();
	}

	// LateUpdate is called after all Update functions have been called
	void LateUpdate () {
		// If health component and display text are assigned, display number of lives remaining on HUD
		if (health && healthDisplay) {
			healthDisplay.text = healthLabel + health.lives.ToString ();
		} else if (!healthDisplay) {
			// Log warning if no display text was assigned
			Debug.LogWarning (gameObject.name + "'s health display not " +
				"assigned! Please assign a text component for the value to display.");
		}

		// If manager component and display text are assigned, display number of enemies remaining on HUD
		if (manager && enemyCountDisplay) {
			enemyCountDisplay.text = enemyCountLabel + manager.enemyCount.ToString ();
		} else if (!enemyCountDisplay) {
			// Log warning if no display text was assigned
			Debug.LogWarning (gameObject.name + "'s enemy count display not " +
				"assigned! Please assign a text component for the value to display.");
		}
	}
}
