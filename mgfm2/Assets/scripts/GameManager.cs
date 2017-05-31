using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager gameManager;
	public static ScoreManager scoreManager;

	static float globalSpeed;


	public GameObject[] rocks;
	public GameObject playerPrefab;
	public GameObject background;
	[HideInInspector]public GameObject player;

	float retryBackgroundTimer = 1.5f;
	GameObject retry;
	GameObject play;

	void Awake() {
		if (gameManager == null)
			gameManager = this;

		if (gameManager != this)
			Destroy(this);
		
		retry = GameObject.Find("Retry");
		retry.SetActive(false);
		play = GameObject.Find("Play");
	}

	void FixedUpdate() {
		if (play.activeSelf) {
			Input.compass.enabled = true;
			background.transform.rotation = Quaternion.Euler(0, 0, Input.compass.magneticHeading);

			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit)) {
					if (hit.transform.name == "Play") {
						play.SetActive(false);
						reinitializeGame();
					}
				}
			}
			return;
		}

		
		Vector3 dir = Vector3.zero;
		dir.y = Input.acceleration.y;
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		dir *= Time.deltaTime;
		globalSpeed = dir.y * 100;



		int health = 0;
		if (player != null)
			health = player.GetComponent<Player>().health;
		GameObject.Find("ScoreText").GetComponent<Text>().text = scoreManager.getScoreText() + "\nHealth: " + health;


		if (retry.activeSelf) {
			if (retryBackgroundTimer <= 0) {
				Input.compass.enabled = true;
				background.transform.rotation = Quaternion.Euler(0, 0, Input.compass.magneticHeading);
			}
			retryBackgroundTimer -= Time.deltaTime;

			GameObject.Find("ScoreText").GetComponent<Text>().text = scoreManager.getScoreText() + "\nHigh Score: " + scoreManager.getHighScore();
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit)) {
					if (hit.transform.name == "Retry") {
						reinitializeGame();
					}
				}
			}
		}
	}


	public void endGame() {
		scoreManager.saveHighScore();
		retry.SetActive(true);
		retryBackgroundTimer = 1.5f;
	}


	public void reinitializeGame() {
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Rock"))
			Destroy(g);
		foreach (GameObject l in GameObject.FindGameObjectsWithTag("Laser"))
			Destroy(l);


		scoreManager = new ScoreManager();
		retry.SetActive(false);
		player = (GameObject)Instantiate(playerPrefab, new Vector3(0, -1.8f, 0), new Quaternion());


		for (int i = 0; i < 5; i++) {
			StartCoroutine(spawnRockRoutine());
		}
	}


	public void spawnRock() {
		StartCoroutine(spawnRockRoutine());
	}


	private IEnumerator spawnRockRoutine() {
		GameObject rock = rocks[Random.Range(0, rocks.Length)];
		float x = Random.Range(-2, 3);
		float y = Random.Range(7, 18);

		yield return new WaitForSeconds(Random.Range(0, 3));

		Instantiate(rock, new Vector3(x, y), Quaternion.identity);
	}

	public static float getGlobalSpeed() {
		return globalSpeed;
	}
}
