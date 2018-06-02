using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Place the script in the Utilities group in the component menu
[AddComponentMenu ("Utilities/Settings Manager")]

/// <summary>
/// Settings controls the user's saved preferences for volume, resolution and
/// graphics quality.
/// </summary>
public class SettingsManager : MonoBehaviour {
	public bool initializeOnLaunch = false;				// State for whether or not to load defaults upon launch

	[Header ("User Preferences")]
	public string username = "User1";					// Used for checking save profiles

	[Header ("Audio Settings")]
	public AudioMixer mixer;							// Reference to the master audio mixer
	public float defaultMasterVolume = 0.8f;			// Default master volume
	public float defaultEffectsVolume = 1.0f;			// Default sound effects volume
	public float defaultMusicVolume = 1.0f;				// Default music volume

	[Header ("Video Settings")]
	public int defaultResolutionX = 1024;				// Default X resolution
	public int defaultResolutionY = 768;				// Default Y resolution
	public enum DefaultDisplayMode {
		windowed, fullscreen							// Display modes
	}
	public DefaultDisplayMode defaultDisplayMode
		= DefaultDisplayMode.windowed;					// Default display mode

	[Header ("Graphics Settings")]
	public int defaultGraphicsQuality = 3;				// Default graphics quality preset

	[Header ("Interface elements")]
	[SerializeField] Slider masterSlider;				// Slider to control master output volume
	[SerializeField] Slider effectsSlider;				// Slider to control sound effect output volume
	[SerializeField] Slider musicSlider;				// Slider to control music output volume
	[SerializeField] Dropdown displayModeDropdown;      // Dropdown menu to select display mode
	[SerializeField] int defaultDisplayModeIndex = 0;	// Reference to the display mode array index of the default display mode
	[SerializeField] Dropdown resolutionDropdown;       // Dropdown menu to select output resolution
	[SerializeField] int defaultResolutionIndex = 7;	// Reference to the resolution array index of the default resolution
	[SerializeField] Dropdown graphicsDropdown;			// Dropdown menu to select graphics quality preset

	Resolution [] resolutions;							// List of all compatible output resolutions
	bool isFullScreen;									// State for whether or not display mode is fullscreen
	bool isDefaultFullscreen;							// State for whether or not display mode is fullscreen by default

	// Used to convert decibel value to linear value
	float DecibelToLinear (float dB) {
		float linear = Mathf.Pow (10.0f, dB / 20.0f);
		return linear;
	}

	// Used to convert linear value to decibel value
	float LinearToDecibel (float linear) {
		float dB;
		if (linear != 0)
			dB = 20.0f * Mathf.Log10 (linear);
		else
			dB = -144.0f;
		return dB;
	}

	// Awake is called when the script instance is being loaded
	void Awake () {
		if (!initializeOnLaunch) {
			// Scan for all available output resolutions
			resolutions = Screen.resolutions;
			if (resolutionDropdown) {
				foreach (Resolution res in resolutions) {
					resolutionDropdown.options.Add
						(new Dropdown.OptionData () { text = res.width + " X " + res.height });
				}
			}
		}

		// If there are no user preferences, load default settings
		if (PlayerPrefs.GetString ("Username") != username) {
			LoadDefaultSettings ();
		}
		// If there are user preferences, load saved settings
		else {
			LoadSettings ();
		}
	}

	// Used to set audio mixer levels
	void SetMixerLevels () {
		// Set saved volume to audio mixer
		mixer.SetFloat ("MasterVolume", LinearToDecibel (PlayerPrefs.GetFloat ("MasterVolume")));
		mixer.SetFloat ("EffectsVolume", LinearToDecibel (PlayerPrefs.GetFloat ("EffectsVolume")));
		mixer.SetFloat ("MusicVolume",  LinearToDecibel (PlayerPrefs.GetFloat ("MusicVolume")));
	}

	// Used to load default settings
	public void LoadDefaultSettings () {
		// Reset volume levels to default value
		PlayerPrefs.SetFloat ("MasterVolume", defaultMasterVolume);
		PlayerPrefs.SetFloat ("EffectsVolume", defaultEffectsVolume);
		PlayerPrefs.SetFloat ("MusicVolume", defaultMusicVolume);

		// Idenfity screen display mode (fullscreen or windowed)
		if (defaultDisplayMode == DefaultDisplayMode.fullscreen) {
			isDefaultFullscreen = true;
		} else {
			isDefaultFullscreen = false;
		}

		// Reset display mode to default value
		PlayerPrefs.SetInt ("DisplayModeIndex", defaultDisplayModeIndex);

		// Reset screen resolution to default value
		Screen.SetResolution (defaultResolutionX, defaultResolutionY, isDefaultFullscreen);

		// Get the array index of the default resolution
		/*if (resolutionDropdown) {
			defaultResolutionIndex = resolutionDropdown.options.IndexOf
				(new Dropdown.OptionData () { text = defaultResolutionX + " X " + defaultResolutionY });
		}*/

		// Set resolution index
		PlayerPrefs.SetInt ("ResolutionIndex", defaultResolutionIndex);

		// Reset username
		PlayerPrefs.SetString ("Username", "User1");

		// Refresh
		LoadSettings ();
	}

	// Used to load settings
	public void LoadSettings () {
		// Set volume sliders
		if (masterSlider) {
			masterSlider.value = PlayerPrefs.GetFloat ("MasterVolume") * masterSlider.maxValue;
		}
		if (effectsSlider) {
			effectsSlider.value = PlayerPrefs.GetFloat ("EffectsVolume") * effectsSlider.maxValue;
		}
		if (musicSlider) {
			musicSlider.value = PlayerPrefs.GetFloat ("MusicVolume") * musicSlider.maxValue;
		}
		if (mixer) {
			SetMixerLevels ();
		}


		// Set display mode
		if (displayModeDropdown) {
			displayModeDropdown.value = PlayerPrefs.GetInt ("DisplayModeIndex");
		}

		// Set resolution dropdown options
		if (resolutionDropdown) {
			resolutionDropdown.value = PlayerPrefs.GetInt ("ResolutionIndex");
		}

		// Set graphic quality dropdown options
		if (graphicsDropdown != null) {
			graphicsDropdown.value = QualitySettings.GetQualityLevel ();
		}

		// Set graphics quality
		QualitySettings.SetQualityLevel (PlayerPrefs.GetInt ("UnityGraphicsQuality"));

		// Load username
		username = PlayerPrefs.GetString ("Username");
	}

	// Used to save settings
	public void SaveSettings () {
		// Save current volume levels
		if (masterSlider) {
			PlayerPrefs.SetFloat ("MasterVolume", masterSlider.value / masterSlider.maxValue);
		}
		if (effectsSlider) {
			PlayerPrefs.SetFloat ("EffectsVolume", effectsSlider.value / effectsSlider.maxValue);
		}
		if (musicSlider) {
			PlayerPrefs.SetFloat ("MusicVolume", musicSlider.value / musicSlider.maxValue);
		}
		if (mixer) {
			SetMixerLevels ();
		}

		// Convert display mode dropdown to boolean
		switch (displayModeDropdown.value) {
			case 0:
				isFullScreen = false;
				break;
			case 1:
				isFullScreen = true;
				break;
		}

		// Save current screen output resolution
		if (displayModeDropdown && resolutionDropdown) {
			PlayerPrefs.SetInt ("DisplayModeIndex", displayModeDropdown.value);
			Screen.SetResolution (resolutions [resolutionDropdown.value].width,
				resolutions [resolutionDropdown.value].height, isFullScreen);
			PlayerPrefs.SetInt ("ResolutionIndex", resolutionDropdown.value);
		}
		
		// Set saved graphics quality
		QualitySettings.SetQualityLevel (graphicsDropdown.value);

		// Save username
		PlayerPrefs.SetString ("Username", username);
	}
}