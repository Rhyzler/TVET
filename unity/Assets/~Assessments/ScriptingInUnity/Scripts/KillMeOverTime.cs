using UnityEngine;

// Place the script in the Utilities group in the component menu
[AddComponentMenu ("Utilities/Kill Me Over Time")]

/// <summary>
/// The Kill Me Over Time component destroys an object after a specified time.
/// </summary>
public class KillMeOverTime : MonoBehaviour {

	public float lifeTime = 1.0f;   // Duration of how long this object will exist

	// Start is called when the instance of an object is initialized
	void Start () {
		// When we are spawned into the level, destroy us after our lifetime
		Destroy (gameObject, lifeTime);
	}
}
