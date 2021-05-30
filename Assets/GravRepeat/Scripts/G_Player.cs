using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G_Player : MonoBehaviour {

	public float GravAcl;//加速度
	public float GravSpd;//速度

	public GameObject bullet;
	public GameObject Last_bullet;
	public int bulNum=1;
	public float bulTime=0;

	public Text beemTxt;
	public GameObject manage;

	enum Enum1 {
		UP,
		DOWN
	}
	Enum1 GravWay;//重力方向

	// Use this for initialization
	void Start () {
		bulNum = 2;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 Pos = this.transform.position;

		Pos.y += GravSpd * Time.deltaTime;

		this.transform.position = Pos;

		if (manage.GetComponent<G_GameManager> ().time > 4) {

			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				GravAcl = 32f;
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				GravAcl = -32f;
			}
			//GravSpd += GravAcl;
			if ((GravAcl > 0 && GravSpd < 0) || (GravAcl < 0 && GravSpd > 0)) {
				GravSpd += GravAcl * 2 * Time.deltaTime;
			} else {
				GravSpd += GravAcl * Time.deltaTime;
			}

			if (Input.GetKeyDown (KeyCode.Z) && bulNum>0 && bulTime<0) {
				Last_bullet = Instantiate (bullet, this.transform.position, new Quaternion (0, 0, 0, 0));
				bulTime = 10f;
				bulNum--;
			}

			if (bulTime >= 0f) {
				bulTime -= Time.deltaTime * 10f;
			}

		}

		beemTxt.text = "Beem:" + bulNum;

	}
}
