using UnityEngine;
using UnityEngine.UI;

// Require a Text component to be attatched
[RequireComponent (typeof (Text))]

/// <summary>
/// UI Slider To Text converts a slider value to a string to display on UI
/// text.
/// </summary>
public class UISliderToText: MonoBehaviour {
	Text myText;							// Reference to object's text component
	
	[SerializeField] Slider slider;			// Reference to slider we are tracking
	[SerializeField] string prefix;			// This goes before our text
	[SerializeField] string suffix;			// This goes after of our text
	[SerializeField] float fraction;		// Used to convert the slider value to a percentage


	void Awake () {
		// Set text component reference
		myText = gameObject.GetComponent<Text> ();
	}

	// Use this for initialization
	void Start () {
		// Update text value
		UpdateValues ();
	}
	
	// Used to update text to string value of slider
	public void UpdateValues () {
		if (myText) {
			// If text was assigned, set text component value
			myText.text = prefix + slider.value / fraction + suffix;
		}
	}
}