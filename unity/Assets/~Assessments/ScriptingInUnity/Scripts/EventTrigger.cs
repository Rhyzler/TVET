using UnityEngine;
using UnityEngine.Events;

// Place the script in group in the component menu
[AddComponentMenu ("Utilities/Event Trigger")]

/// <summary>
/// The Event on Trigger component triggers a Unity Event upon colliding with a
/// trigger.
/// </summary>
public class EventTrigger : MonoBehaviour {
	[SerializeField] string [] triggerTags;	// Tags used to trigger event
	[SerializeField] UnityEvent onTouch;    // Events to occur when triggered

	// OnTriggerEnter is called when the Collider other enters the trigger
	void OnTriggerEnter (Collider other) {

		// Check for trigger tags
		foreach (string tag in triggerTags) {
			if (other.tag == tag) {
				// Trigger events
				onTouch.Invoke ();
			}
		}
	}
}
