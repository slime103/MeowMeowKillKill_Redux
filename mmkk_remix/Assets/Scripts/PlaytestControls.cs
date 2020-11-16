using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaytestControls : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.LeftShift)) {
			
			if (Input.GetKeyDown(KeyCode.R)) {
				Debug.Log("Reloading current level");
				ReloadCurrentLevel();
			}

			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				Debug.Log("Loading previous level");
				LoadPreviousLevel();
			}

			if (Input.GetKeyDown(KeyCode.Alpha2)) {
				Debug.Log("Loading next level");
				LoadNextLevel();
			}

			if (Input.GetKeyDown(KeyCode.Alpha0)) {
				Debug.Log("Loading first level");
				LoadFirstLevel();
			}
		}
	}

	void LoadScene (int sceneIndex) {
		Debug.Log("scene " + SceneManager.GetActiveScene().name + " time: " + Time.timeSinceLevelLoad);
		SceneManager.LoadScene(sceneIndex);
	}

	void ReloadCurrentLevel () {
		LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	void LoadPreviousLevel () {
		int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
		if (sceneIndex < 0) {
			sceneIndex = SceneManager.sceneCountInBuildSettings - 1;
		}

		LoadScene(sceneIndex);
	}

	void LoadNextLevel () {
		int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (sceneIndex > SceneManager.sceneCountInBuildSettings - 1) {
			sceneIndex = 0;
		}

		LoadScene(sceneIndex);
	}

	void LoadFirstLevel () {
		LoadScene(0);
	}
}
