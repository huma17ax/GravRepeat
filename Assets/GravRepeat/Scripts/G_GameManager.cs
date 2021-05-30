using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class G_GameManager : MonoBehaviour {

	public bool PlayerAlive;

	public int Score;
	public int highScore;

	public int aligFlag;
	public float aligY;

	public Text scoreTxt;
	public Text cntDownTxt;
	public Text GOsign;
	public GameObject spawn;

	public float time=0,preTime;

	// Use this for initialization
	void Start () {

		PlayerAlive = true;
		Score = 0;

		aligFlag = 0;
		aligY = 0f;

		GOsign.text = "";

		string filepath = Application.dataPath+@"/Score.txt";
		string score_str = File.ReadAllText (filepath);
		highScore = int.Parse (score_str);
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreTxt.text = "Score:" + Score;

		if (time < 5) {
			preTime = time;
			time += Time.deltaTime;

			if (preTime < 1 && time > 1) {
				cntDownTxt.text = "2...";
			}
			if (preTime < 2 && time > 2) {
				cntDownTxt.text = "1...";
				Instantiate (spawn, this.transform.position, this.transform.rotation);
			}
			if (preTime < 3 && time > 3) {
				cntDownTxt.text = "Start!";
			}
			if (preTime < 4 && time > 4) {
				cntDownTxt.text = "";
			}
		}

		if (PlayerAlive == false) {
			if (highScore < Score) {
				cntDownTxt.text = "HighScore!";
			} else {
				cntDownTxt.text = "GameOver";
			}
			GOsign.text = "press 'A' to Re:start\npress 'X' to Title";

			if (Input.GetKeyDown (KeyCode.A)) {
				if (highScore < Score) {
					highScore = Score;
				}
				string filepath = Application.dataPath+@"/Score.txt";
				File.WriteAllText (filepath, "" + highScore);
				SceneManager.LoadScene ("GravRepeat");
			}
			if (Input.GetKeyDown (KeyCode.X)) {
				if (highScore < Score) {
					highScore = Score;
				}
				string filepath = Application.dataPath+@"/Score.txt";
				File.WriteAllText (filepath, "" + highScore);
				SceneManager.LoadScene ("GravRepeat");
				SceneManager.LoadScene ("Title");
			}

		}
	}

	void OnGUI(){
	}
}
