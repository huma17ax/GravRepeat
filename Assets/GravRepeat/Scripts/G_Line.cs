using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Line : MonoBehaviour {

	public float AdvanceSpeed = 16f;
	GameObject player;
	GameObject particles;
	GameObject manager;
	float prevZ;
	bool aligFlag;
	float aligY;

	public Material matUP,matDOWN;

	enum Enum1 {UP,DOWN};
	Enum1 way;
	// Use this for initialization
	void Start () {

		if (GameObject.Find ("Player") != null) {
			player = GameObject.Find ("Player").gameObject;
		}
		manager = GameObject.Find ("GameManager").gameObject;

		if (Random.Range (0f, 1f) > 0.5f) {
			way = Enum1.UP;
			particles = transform.Find ("ParticleUP").gameObject;
			this.GetComponent<Renderer> ().material = matUP;
		} else {
			way = Enum1.DOWN;
			particles = transform.Find ("ParticleDOWN").gameObject;
			this.GetComponent<Renderer> ().material = matDOWN;
		}
		particles.SetActive (true);

		prevZ = this.transform.position.z;
		
	}
	
	// Update is called once per frame
	void Update () {
		AdvanceSpeed = 16f;

		Vector3 Pos = this.transform.position;
		Pos.z -= AdvanceSpeed * Time.deltaTime;
		/*
		if (this.transform.position.z < 2f && this.transform.position.z > -2f) {
			if (Input.GetKeyDown (KeyCode.Z)) {
				Destroy (this.gameObject, 0);
			}
		}
		if (this.transform.position.z < -2f) {
			Destroy (player, 0);
		}*/

		if (aligFlag) {
			if (this.transform.position.y > aligY) {
				Pos.y -= 35f * Time.deltaTime;
			} else {
				Pos.y += 35f * Time.deltaTime;
			}
			if (Mathf.Abs (this.transform.position.y - aligY) < 35f * Time.deltaTime) {
				//Pos.y = aligY;
				aligFlag = false;
			}
		}

		this.transform.position = Pos;

		if (prevZ > 0f && this.transform.position.z < 0f) {
			if (way == Enum1.UP) {
				if (player.GetComponent<G_Player> ().GravAcl > 0) {
					Destroy (this.gameObject, 0);
				} else {
					Destroy (player, 0);
					manager.GetComponent<G_GameManager> ().PlayerAlive = false;
				}
			}
			if (way == Enum1.DOWN) {
				if (player.GetComponent<G_Player> ().GravAcl < 0) {
					Destroy (this.gameObject, 0);
				} else {
					Destroy (player, 0);
					manager.GetComponent<G_GameManager> ().PlayerAlive = false;
				}
			}
		}

		if (manager.GetComponent<G_GameManager> ().PlayerAlive) {
			if (Mathf.Abs (player.GetComponent<G_Player> ().Last_bullet.transform.position.z - this.transform.position.z) < 1) {
				aligFlag = true;
				aligY = player.GetComponent<G_Player> ().Last_bullet.transform.position.y;
			}
		}

		prevZ = this.transform.position.z;
	}
}
