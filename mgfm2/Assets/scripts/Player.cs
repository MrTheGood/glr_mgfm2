using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public float laserCooldown = 1;
	public Vector3[] laserSpawnLocations;
	public GameObject laser;
	public AudioClip fireLaserSound;

	public float speed = 15;
	public int health = 10;

	private bool alive = true;
	private Animator animator;
	private AudioSource audioSource;
	private int lastLaserSpawnLocation = 0;
	private float startLaserCooldown;

	void Start() {
		startLaserCooldown = laserCooldown;
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	void FixedUpdate () {
		if (!alive)
			return;
		
		laserCooldown -= Time.fixedDeltaTime;
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && laserCooldown <= 0)
			fireLasers();


		Vector3 dir = Vector3.zero;
		dir.x = Input.acceleration.x;
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		dir *= Time.deltaTime;
		dir *= speed;

		if (transform.position.x + dir.x > -2.2f && transform.position.x + dir.x < 2.2) {
			transform.Translate(dir);
		}

		if (dir.x > 0.05) {
			animator.SetInteger("directionState", 2);
		} else if (dir.x < -0.05) {
			animator.SetInteger("directionState", 1);
		} else {
			animator.SetInteger("directionState", 0);
		}
	}

	void OnCollisionEnter2D(Collision2D c) {
		Entity e = c.gameObject.GetComponent<Entity>();

		if (e == null)
			return;
		
		if (getHit(e.dmg) <= 0)
			die();

		Destroy(c.gameObject);
		GameManager.gameManager.spawnRock();
	}

	private void die() {
		animator.SetTrigger("explode");
		alive = false;
		health = 0;
		Destroy(gameObject, .45f);
		GameManager.gameManager.endGame();
	}

	private int getHit(int dmg) {
		health -= dmg;
		return health;
	}


	private void fireLasers() {
		lastLaserSpawnLocation++;
		if (lastLaserSpawnLocation >= laserSpawnLocations.Length)
			lastLaserSpawnLocation = 0;
		laserCooldown = startLaserCooldown;
		
		Instantiate(laser, transform.position + laserSpawnLocations[lastLaserSpawnLocation], Quaternion.identity);
		audioSource.clip = fireLaserSound;
		audioSource.Play();
	}


}
