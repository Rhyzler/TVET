using UnityEngine;

// Place the script in the Utilities group in the component menu
[AddComponentMenu ("Utilities/Controller Input Manager")]

/// <summary>
/// Controller Manager detects controller inputs and controls cursor locking.
/// </summary>
public class ControllerManager : MonoBehaviour {

	public static bool gamepadConnected;							// Determines whether or not gamepad is connected
	[SerializeField] bool initializeOnStart = true;					// State for whether or not we want to set cursor lock mode on start
	public CursorLockMode cursorLockMode = CursorLockMode.Locked;	// Set cursor lock mode for when we are paused

	// Awake is called when the script instance is being loaded
	void Awake () {
		// If joysticks are found, we are using controllers
		if (Input.GetJoystickNames ().Length > 0 && Input.GetJoystickNames () [0] != "") {
			gamepadConnected = true;
		}
		// If no joysticks are found, we aren't using controllers
		else {
			gamepadConnected = false;
		}

		// Set cursor lock state
		if (initializeOnStart) {
			Cursor.lockState = cursorLockMode;
		}
	}

	public void LockCursor () {
		// If we want to lock cursor, lock cursor
		if (cursorLockMode != CursorLockMode.None) {
			switch (cursorLockMode) {
				case CursorLockMode.Confined:
					Cursor.lockState = CursorLockMode.Confined;
					break;
				case CursorLockMode.Locked:
					Cursor.lockState = CursorLockMode.Locked;
					break;
			}
			// Hide cursor when locking
			Cursor.visible = false;
		}
	}

	public void UnlockCursor () {
		// If cursor is locked, unlock uncursor
		if (cursorLockMode != CursorLockMode.None && !gamepadConnected) {
			Cursor.lockState = CursorLockMode.None;
			// Show cursor
			Cursor.visible = true;
		}
	}
}