using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Bullet : MonoBehaviour {

	float speed;
	G_GameManager g_gmaneg;

	// Use this for initialization
	void Start () {
		speed =100f;
		g_gmaneg = GameObject.Find ("GameManager").gameObject.GetComponent<G_GameManager>();
		g_gmaneg.aligFlag = 1;
		g_gmaneg.aligY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 Pos = this.transform.position;
		Pos.z+=speed * Time.deltaTime;

		this.transform.position = Pos;

		if (this.transform.position.z > 100) {
			Destroy (this.gameObject);
		}
	}
}
