using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaenAscensor : MonoBehaviour {
	
	public GameObject arriba;
	public int speed;

	void Start(){
		arriba = GameObject.FindGameObjectWithTag ("emptyAscensor");
		StartCoroutine ("Parriba");
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "suelo") {
			Destroy (this.gameObject);
		}
	}

	IEnumerator Parriba (){
		while (this.transform.position.y < arriba.transform.position.y) {
			this.transform.Translate (0, speed* Time.deltaTime, 0);
			yield return new WaitForFixedUpdate ();
		}
		speed = 0;
	}
}
