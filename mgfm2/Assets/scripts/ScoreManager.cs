using UnityEngine;
using System.Collections;

public class ScoreManager {
	private int highScore;
	private int score;

	public ScoreManager() {
		highScore = 0;
		if (PlayerPrefs.HasKey("highScore"))
			highScore = PlayerPrefs.GetInt("highScore");
	}


	public void saveHighScore() {
		if (score > highScore) {
			PlayerPrefs.SetInt("highScore", score);
			PlayerPrefs.Save();
		}
	}

	public int getHighScore() {
		return highScore;
	}



	public int addScore(int score) {
		score += Mathf.RoundToInt(GameManager.getGlobalSpeed());

		return this.score += score;
	}

	public int getScore() {
		return this.score;
	}

	public string getScoreText() {
		return "Score: " + this.score;
	}
}
