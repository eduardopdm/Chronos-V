using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogidaEnergia : MonoBehaviour {
	public Animator miAnimator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		Debug.Log ("entren");
		if (other.gameObject.tag == "Player") {
			miAnimator.SetTrigger ("entre");
			Destroy (gameObject, 0.25f);
		}
	}
}
