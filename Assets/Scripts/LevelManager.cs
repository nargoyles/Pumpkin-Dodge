using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadNextLevel(string level) {
		Application.LoadLevel (level);
		Score.ResetScore ();
	}

	public void Quit() {
		Application.Quit ();
	}
}
