using UnityEngine;

// Require a Character Controller component to be attatched
[RequireComponent (typeof (CharacterController))]

// Place the script in group in the component menu
[AddComponentMenu ("Player/Movement")]

/// <summary>
/// Player Movement controls the movement of the player character. This
/// component allows a user-controlled character to move left, right, forward
/// and backwards in 3-d space.
/// </summary>
public class PlayerMovement : MonoBehaviour {

	[SerializeField] float speed;				// Speed at which the player moves
	[SerializeField] string horizontalInputName
		= "Horizontal";                         // Name used to identify horizontal input axis
	[SerializeField]
	string verticalInputName
		= "Vertical";							// Name used to identify vertical input axis

	CharacterController controller;				// Reference to Character Controller component

	void Start () {
		// Assign Character Controller
		controller = transform.GetComponent<CharacterController> ();
	}

	void Update () {
		// Read inputs
		float moveHorizontal = Input.GetAxis (horizontalInputName);
		float moveVertical = Input.GetAxis (verticalInputName);

		// Calculate movement direction
		Vector3 movement = transform.TransformDirection (new Vector3 (moveHorizontal, 0.0f, moveVertical));

		// Add speed to our movement
		movement = movement * speed;

		// Move the player
		controller.SimpleMove (movement);
	}
}