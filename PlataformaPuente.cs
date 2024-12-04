using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaPuente : MonoBehaviour {

	public GameObject plataformaPref;
	// Use this for initialization
	void Start () {
	}
	
	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == "Player") {
			Invoke ("Caida", 0.3f);
		}
	}

	void Caida (){
		GameObject plataformaQueCae = Instantiate (plataformaPref, this.transform.position, this.transform.rotation) as GameObject;
		Destroy (plataformaQueCae.gameObject, 10);
		Destroy (this.gameObject);
	}
}
