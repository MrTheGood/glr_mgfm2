using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	public float ySpeed = 8;
	public int dmg = 1;

	void FixedUpdate () {
		transform.Translate(Vector3.up * (ySpeed + GameManager.getGlobalSpeed()) * Time.deltaTime);

		if (!GetComponent<SpriteRenderer>().isVisible)
			Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D c) {
		Entity e = c.gameObject.GetComponent<Entity>();
		if (e == null)
			return;

		e.getHit(dmg);
		Destroy(gameObject);
	}
}
