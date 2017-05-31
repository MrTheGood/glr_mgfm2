using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	
	void FixedUpdate () {
		Vector3 dir = Vector3.zero;
		dir.x = -Input.acceleration.y;
		dir.z = Input.acceleration.x;

		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		dir *= Time.deltaTime;
		transform.Translate(dir * 10);
	}
}
