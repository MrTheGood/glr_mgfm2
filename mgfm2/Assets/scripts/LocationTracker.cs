using UnityEngine;
using System.Collections;

public class LocationTracker : MonoBehaviour {

	public static IEnumerator Start() {	//TODO:  Does not work
		//Is location enabled?
		if (!Input.location.isEnabledByUser) {
			print("Please enable location tracking");
			yield break;
		}

		Input.location.Start();

		int waitTimeout = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && waitTimeout > 0) {
			yield return new WaitForSeconds(1);
			waitTimeout--;
		}

		if (waitTimeout < 1) {
			print("Timed out..");
			yield break;
		}
		if (Input.location.status == LocationServiceStatus.Failed) {
			print("Unable to get the device location..");
			yield break;
		} else {
			print("Location: " + Input.location.lastData.latitude + " - " + Input.location.lastData.longitude);
		}

		Input.location.Stop();
	}
}
