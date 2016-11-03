using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
	public static int score;
	public Text text;
	public int highScore;
	string highScoreKey = "HighScore";
	
	void Awake ()
	{
		text = GameObject.FindGameObjectWithTag ("score").GetComponent <Text> ();
	}

	void Start(){
		//Get the highScore from player prefs if it is there, 0 otherwise.
		highScore = PlayerPrefs.GetInt(highScoreKey,0);    
	}
	
	public static void ResetScore() {
		score = 0;
	}

	void OnDisable(){
		
		//If our scoree is greter than highscore, set new higscore and save.
		if(score>highScore){
			PlayerPrefs.SetInt(highScoreKey, score);
			PlayerPrefs.Save();
		}
	}
	
	void Update ()
	{
		if (Application.loadedLevelName == "Dodge") {
			text.text = "Score: " + score;
		}
		if (Application.loadedLevelName == "Dead") {
			text.text = "Game Over\nYour score is " + score +"\nHigh score is " + highScore;
		}
	}
}