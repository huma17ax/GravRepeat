using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Title : MonoBehaviour {

	bool startFlag = false;

	float speed=1f;

	public GameObject image;
	public GameObject image2;
	public Text scoreTxt;

	float red1,green1,blue1,red2,green2,blue2;

	float alfa=1;

	int highScore;

	void Awake(){
		Screen.SetResolution (800, 600, false);
	}

	// Use this for initialization
	void Start () {

		red1 = image.GetComponent<Image> ().color.r;
		green1 = image.GetComponent<Image> ().color.g;
		blue1 = image.GetComponent<Image> ().color.b;

		red2 = image2.GetComponent<Image> ().color.r;
		green2 = image2.GetComponent<Image> ().color.g;
		blue2 = image2.GetComponent<Image> ().color.b;

		string filepath = Application.dataPath+@"/Score.txt";
		//Debug.Log (filepath);
		//存在しなければ作成
		if (!File.Exists (filepath)) {
			using (File.Create (filepath)) {
			}
		}
		string score_str = File.ReadAllText (filepath);

		//Debug.Log (score_str);

		highScore = int.Parse (score_str);

		scoreTxt.text = "HighScore:" + highScore;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit ();
			} else {
				startFlag = true;
				scoreTxt.text = "";
			}
		}


		if (startFlag) {
			image.GetComponent<Image>().color = new Color(red1, green1, blue1, alfa);
			image2.GetComponent<Image>().color = new Color(red2, green2, blue2, alfa);
			alfa -= speed * Time.deltaTime;

			if (alfa < 0f) {
				SceneManager.LoadScene ("GravRepeat");
			}
		}

	}
}
