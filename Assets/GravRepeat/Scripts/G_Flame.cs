using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Flame : MonoBehaviour {

	public float AdvanceSpeed = 16f;
	float prevZ;
	GameObject player;
	public GameObject manager;

	public bool aligFlag;
	public float aligY;

	public Material UPWallMat;
	public Material LowWallMat;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player").gameObject;
		manager = GameObject.Find ("GameManager").gameObject;

		UPWallMat = gameObject.transform.Find ("UpSide").GetComponent<Renderer> ().material;
		LowWallMat = gameObject.transform.Find ("DownSide").GetComponent<Renderer>().material;

		//色設定
		/*
		UPQuadMat.SetColor("_TopColor",new Color(255f/255f, 0f/255f, 0f/255f));
		UPQuadMat.SetColor("_ButtomColor",new Color(255f/255f, 165f/255f, 223f/255f));
		UPQuadMat.SetFloat ("_TopColorAmount", 0.5f);

		LowQuadMat.SetColor("_TopColor",new Color(154f/255f, 219f/255f, 255f/255f));
		LowQuadMat.SetColor("_ButtomColor",new Color(0f/255f, 0f/255f, 255f/255f));
		LowQuadMat.SetFloat ("_TopColorAmount", 0.5f);
		*/
		UPWallMat.color = new Color (255f / 255f, 165f / 255f, 223f / 255f) * (30f - Mathf.Abs (this.transform.position.z)) / 10;
		LowWallMat.color = new Color (154f / 255f, 219f / 255f, 255f / 255f) * (30f - Mathf.Abs (this.transform.position.z)) / 10;

		prevZ = this.transform.position.z;
		aligFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

		AdvanceSpeed = 16f;
		/*
		if (Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
			AdvanceSpeed = 0.15f;
		}
		if (!Input.GetKey (KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) {
			AdvanceSpeed = 0.25f;
		}
		*/

		Vector3 Pos = this.transform.position;
		Pos.z -= AdvanceSpeed * Time.deltaTime;

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

		if (transform.position.z < -30) {
			Destroy (this.gameObject, 0);
		}

		//色グラデーション
		if (this.transform.position.z > 20 || this.transform.position.z < -20) {
			UPWallMat.color = new Color (255f / 255f, 165f / 255f, 223f / 255f) * (30f - Mathf.Abs (this.transform.position.z)) / 10;
			LowWallMat.color = new Color (154f / 255f, 219f / 255f, 255f / 255f) * (30f - Mathf.Abs (this.transform.position.z)) / 10;
		} else {
			UPWallMat.color = new Color (255f / 255f, 165f / 255f, 223f / 255f);
			LowWallMat.color = new Color (154f / 255f, 219f / 255f, 255f / 255f);
		}

		//当たり判定
		if (this.transform.position.z < 0.5f && this.transform.position.z > -0.5f) {
			if (Mathf.Abs (this.transform.position.y - player.transform.position.y) > 10) {
				Destroy (player, 0);
				manager.GetComponent<G_GameManager> ().PlayerAlive = false;
			}
		}

		if (prevZ > 0f && this.transform.position.z < 0f) {
			if (manager.GetComponent<G_GameManager> ().PlayerAlive == true) {
				manager.GetComponent<G_GameManager> ().Score += 1;
			}
		}

		if (player.GetComponent<G_Player>().bulTime>=0f && aligFlag==false && Mathf.Abs (this.transform.position.y - player.GetComponent<G_Player> ().Last_bullet.transform.position.y) > 35f * Time.deltaTime && this.transform.position.z > 0f && player.GetComponent<G_Player> ().Last_bullet.transform.position.z > this.transform.position.z) {
			aligFlag = true;
			aligY = player.GetComponent<G_Player> ().Last_bullet.transform.position.y;
		}

		prevZ = this.transform.position.z;

		SetColor ();
		
	}

	void SetColor(){


	}
}
