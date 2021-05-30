using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Energy : MonoBehaviour {

	float speed=16f;

	GameObject player;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("Player") != null) {
			player = GameObject.Find ("Player").gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Rotate (new Vector3 (2, 1, -2));

		speed = 16f;

		Vector3 Pos = this.transform.position;
		Pos.z -= speed * Time.deltaTime;
		this.transform.position = Pos;

		if (Vector3.Distance(this.transform.position , player.transform.position) < 2) {
			player.GetComponent<G_Player> ().bulNum++;
			Destroy (this.gameObject);
		}
	}
}
