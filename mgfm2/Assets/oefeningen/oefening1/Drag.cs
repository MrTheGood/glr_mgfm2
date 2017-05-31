using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {

	void FixedUpdate() {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchPosition = Input.GetTouch(0).position;
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 10));
		}
	}
}
