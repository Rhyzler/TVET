using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Place the script in the UI in the component menu
[AddComponentMenu ("UI/Menu Navigation")]

/// <summary>
/// The Menu Control component contains core menu navigaion functionality.
/// This component allows for a certain level from an index or by name,
/// and for the application to exit.
/// </summary>
public class MenuControl : MonoBehaviour {

	// Load level by scene index
	public void LoadSceneByIndex (int level) {
		SceneManager.LoadScene (level);
	}

	// Load level by scene name
	public void LoadSceneByName (string level) {
		SceneManager.LoadScene (level);
	}

	// Reloads the current scene
	public void ReloadScene () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	// Exit the application
	public void Quit () {
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit ();
#endif
	}
}
