using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	private bool grounded = true;
	
	
	void FixedUpdate () {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			if (grounded) {
				this.GetComponent<Rigidbody>().AddForce(0, 4, 0, ForceMode.Impulse);
			}
		}

		if (Utils.getScore() > 5) {
			print("Score is greater than five");
		}
	}

	void OnCollisionEnter() {
		grounded = true;
	}

	void OnCollisionExit() {
		grounded = false;
		print(Utils.addScore(1));
	}
}
