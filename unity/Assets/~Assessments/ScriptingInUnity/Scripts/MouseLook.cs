using UnityEngine;

// Place the script in the Camera Control group in the component menu
[AddComponentMenu ("Camera Control/Mouse Look")]

/// <summary>
/// The Mouse Look component rotates an object using mouse or joystick movement
/// input.
/// </summary>
/// 
public class MouseLook : MonoBehaviour {

	public enum RotationAxes {
		MouseXAndY = 0,									// Read input from both Mouse X and Mouse Y
		MouseX = 1,										// Read input only from Mouse X
		MouseY = 2										// Read input only from Mouse Y
	};
	public RotationAxes axes = RotationAxes.MouseXAndY; // Used to set which type of rotation axes to use for rotation
	public float sensitivityX = 15F;					// X-axis rotation sensitivity
	public float sensitivityY = 15F;					// Y-axis rotation sensitivity

	public float minimumX = -360F;						// Minimum rotation degrees along x axis
	public float maximumX = 360F;                       // Maximum rotation degrees along x axis

	public float minimumY = -60F;                       // Minimum rotation degrees along y axis
	public float maximumY = 60F;                        // Maximum rotation degrees along y axis

	float rotationX = 0F;								// Used to track current rotation along x axis
	float rotationY = 0F;                               // Used to track current rotation along y axis

	Quaternion originalRotation;						// Used to store original rotation to calculate new rotation amount

	void Update () {
		if (axes == RotationAxes.MouseXAndY) {
			// Read the Mouse X and Mouse Y input axes and multipy by sensitivity
			rotationX += Input.GetAxis ("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;

			// Clamp rotation between minumum and maximum rotation angles
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			rotationY = ClampAngle (rotationY, minimumY, maximumY);

			// Apply rotation amount
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);

			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		} else if (axes == RotationAxes.MouseX) {
			// Read the Mouse X input axis and multipy by sensitivity
			rotationX += Input.GetAxis ("Mouse X") * sensitivityX;

			// Clamp rotation between minumum and maximum rotation angles
			rotationX = ClampAngle (rotationX, minimumX, maximumX);

			// Apply rotation amount
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		} else {
			// Read the Mouse Y input axis and multipy by sensitivity
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;

			// Clamp rotation between minumum and maximum rotation angles
			rotationY = ClampAngle (rotationY, minimumY, maximumY);

			// Apply rotation amount
			Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}

	void Start () {
		// Freeze rotations on rigidbody, if any
		if (GetComponent<Rigidbody> ())
			GetComponent<Rigidbody> ().freezeRotation = true;
		originalRotation = transform.localRotation;
	}

	// Method used to clamp angle between minumum and maximum values
	public static float ClampAngle (float angle, float min, float max) {
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}
