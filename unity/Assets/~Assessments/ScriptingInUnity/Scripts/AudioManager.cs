using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

// Place the script in the Utilities group in the component menu
[AddComponentMenu ("Utilities/Audio Manager")]

/// <summary>
/// Audio Manager sets the volume for music and sound effect sources to their
/// saved values.
/// </summary>
public class AudioManager : MonoBehaviour {
	[SerializeField] AudioMixerGroup effectsGroup;	// Reference to effects mixer group
	[SerializeField] AudioMixerGroup musicGroup;    // Reference to music mixer group

	List<AudioSource> effectsSources;				// Array of sound effect sources
	List<AudioSource> musicSources;					// Array of music sources

	// Use this for initialization
	public void Awake () {
		// Find all audio sources and add them to an array
		AudioSource [] sources = FindObjectsOfType<AudioSource> () as AudioSource [];
		
		// Add sources that output through effects group to effects list
		foreach (AudioSource source in sources) {
			if (source.outputAudioMixerGroup == effectsGroup) {
				effectsSources.Add (source);
			}
		}

		// Add sources that output through music group to music list
		foreach (AudioSource source in sources) {
			if (source.outputAudioMixerGroup == musicGroup) {
				musicSources.Add (source);
			}
		}

		// Set our volumes
		SetVolume ();
	}

	// Used to set mixer levels
	public void SetVolume () {
		// Set music volume
		if (musicSources != null) { 
			foreach (AudioSource mV in musicSources) {
				mV.volume = PlayerPrefs.GetFloat ("MusicVolume");
			}
		}

		// Set effects volume
		if (effectsSources != null) {
			foreach (AudioSource eV in effectsSources) {
				eV.volume = PlayerPrefs.GetFloat ("EffectsVolume");
			}
		}
	}
}