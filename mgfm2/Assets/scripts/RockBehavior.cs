using UnityEngine;
using System.Collections;

public class RockBehavior : Entity {
	public float ySpeed = 5;

	private float rotationSpeed;

	void Start() {
		rotationSpeed = (int)Random.Range(-180, 180);
	}

	new void FixedUpdate() {
		base.FixedUpdate();

		transform.position = new Vector3(transform.position.x, transform.position.y - (ySpeed + GameManager.getGlobalSpeed()) * Time.deltaTime);
		transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);

		if (transform.position.y < -4)
			deleteRock();
	}

	void OnCollisionEnter2D(Collision2D c) {
		if (c.transform.tag == "Rock") {
			deleteRock();
		}
	}

	public void deleteRock() {
		GameManager.gameManager.spawnRock();
		Destroy(gameObject);
	}
}
