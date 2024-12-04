using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "suelo") {
			Destroy (this.gameObject);
		}
	}
}
