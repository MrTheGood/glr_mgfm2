using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utils {
	static int totalScore = 0;

	public static int addScore(int score) {
		return totalScore += score;
	}

	public static int getScore() {
		return totalScore;
	}
}
