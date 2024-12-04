using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetBonbona : MonoBehaviour {
	public Material bonbona;

	
	// Update is called once per frame
	void Update () {
		//GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (0, 0.5f);
		bonbona.mainTextureOffset += new Vector2 ( 0.1f*Time.deltaTime,0);
	}
}
