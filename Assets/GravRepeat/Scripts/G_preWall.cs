﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_preWall : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 Pos = this.transform.position;
		Pos.y = player.transform.position.y;
		this.transform.position = Pos;
	}
}
