using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	public int health = 5;
	public int points = 5;
	public int dmg = 5;


	public int getHit(int dmg) {
		health -= dmg;
		return health;
	}

	public void die() {
		GameManager.scoreManager.addScore(points);
		GameManager.gameManager.spawnRock();
		Destroy(gameObject);
	}

	protected void FixedUpdate() {
		if (health <= 0)
			die();
	}
}
