using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Spawn : MonoBehaviour {

	int GameMode=0;
	//0...ノーマル　1...ハード1キー　2...ハード2キー　3...ハード3キー

	public GameObject flame;
	private GameObject LastFlame;
	public GameObject Line;
	public GameObject Energy;
	GameObject manager;

	float SpawnY=0;

	float Range=0;//今の進行時間
	float SetRange=0;//次の切り替えまでの間隔
	float way=0;//上下

	public float aligWait;
	public float aligY;
	public int aligState;

	int span=0;

	// Use this for initialization
	void Start () {
		SetRange = Random.Range (0f, 1f) * 50 + 20;

		manager = GameObject.Find ("GameManager").gameObject;

		aligWait = 100f;
		aligY = 0;
		aligState = 2;

		//最初の1フレーム
		Vector3 Pos = new Vector3 (-20, SpawnY, 30);
		Quaternion Rot = Quaternion.Euler (0, 0, 0);
		LastFlame = Instantiate (flame, Pos, Rot);
	}
	
	// Update is called once per frame
	void Update () {

		if (LastFlame.transform.position.z<=29 && manager.GetComponent<G_GameManager>().PlayerAlive) {
			Vector3 Pos = new Vector3 (-20, SpawnY, LastFlame.transform.position.z + 1f - 16f*Time.deltaTime);
			Quaternion Rot = Quaternion.Euler (0, 0, 0);
			LastFlame = Instantiate (flame, Pos, Rot);
		}

		if (manager.GetComponent<G_GameManager>().time>5 && manager.GetComponent<G_GameManager>().PlayerAlive) SpawnLine (GameMode);

		if (manager.GetComponent<G_GameManager> ().aligFlag==1) {
			aligWait = 100f;
			aligY = manager.GetComponent<G_GameManager> ().aligY;
			manager.GetComponent<G_GameManager> ().aligFlag = 0;
		}

		if (aligWait>0) {
			SpawnY = aligY + (70f * Random.Range(0f,1f) - 35f) * Time.deltaTime;
			if (Random.Range (0f, 1f)>0.5f) {
				way = 0;
			} else {
				way = 1;
			}
			Range = 0;
			SetRange = Random.Range (0f, 1f) * 50 + 20;
			aligWait-=Time.deltaTime*75;
		} else {
			SetY ();
		}

	}

	void SetY(){
		Range++;

		if (way==0) {
			SpawnY += 30f * (1 - (Mathf.Abs (SetRange / 2 - Range) / (SetRange / 2))) * Time.deltaTime;
		} else {
			SpawnY -= 30f * (1 - (Mathf.Abs (SetRange / 2 - Range) / (SetRange / 2))) * Time.deltaTime;
		}

		if (Range > SetRange) {
			if (way == 0) {
				way = 1;
			} else {
				way = 0;
			}
			Range = 0;
			SetRange = Random.Range (0f, 1f) * 100 + 40;

		}
	}

	void SpawnLine(int mode){

		if (span==0 && Random.Range (0f, 1f) < 0.005f) {
			Vector3 Pos = new Vector3 (0, SpawnY, LastFlame.transform.position.z + (1f - 16f*Time.deltaTime)/2);
			Quaternion Rot = Quaternion.Euler (0, 0, 0);
			Instantiate (Line, Pos, Rot);
			if (Random.Range (0f, 1f) < 0.1f) {
				Instantiate (Energy, Pos, Random.rotation);
			}

			span = 30;
		}
		if (span > 0) {
			span--;
		}

	}

	void OnGUI () {
	}
}
