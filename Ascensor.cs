using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascensor : MonoBehaviour {

	public GameObject enemigoPrefab;
	public GameObject plataforma;
	public GameObject arriba;
	public int speed;

	public GameObject spawn1;
	public GameObject spawn2;
	public GameObject spawn3;
	public GameObject spawn4;

	public bool subiendo = false;
	private bool haSubidoYa = false;
	public float contador = 0;
	// Use this for initialization

	void Update (){
		if (plataforma.transform.position.y < arriba.transform.position.y && subiendo == true) {
			contador += Time.deltaTime;
		}
	}
	// Update is called once per frame
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player" && haSubidoYa == false) {
			haSubidoYa = true;
			StartCoroutine ("CorrutinaEnemigos");
			StartCoroutine ("Subir");
		}
	}

	IEnumerator Subir (){
		subiendo = true;
		while (plataforma.transform.position.y < arriba.transform.position.y) {
			plataforma.transform.Translate (0, speed* Time.deltaTime, 0);
			yield return new WaitForFixedUpdate ();
		}
		subiendo = false;
	}

	IEnumerator CorrutinaEnemigos (){
		GameObject enemigo = Instantiate (enemigoPrefab, spawn2.transform.position, spawn1.transform.rotation) as GameObject;
		yield return new WaitForSeconds (3);
		GameObject enemigo2 = Instantiate (enemigoPrefab, spawn1.transform.position, spawn1.transform.rotation) as GameObject;
		yield return new WaitForSeconds (4);
		GameObject enemigo3 = Instantiate (enemigoPrefab, spawn3.transform.position, spawn1.transform.rotation) as GameObject;
		yield return new WaitForSeconds (3);
		GameObject enemigo4 = Instantiate (enemigoPrefab, spawn4.transform.position, spawn1.transform.rotation) as GameObject;
		yield return new WaitForSeconds (3);
		GameObject enemigo5 = Instantiate (enemigoPrefab, spawn2.transform.position, spawn1.transform.rotation) as GameObject;
	}
	//GameObject enemigo = Instantiate (enemigoPrefab, spawn1.transform.position, spawn1.transform.rotation) as GameObject;
	//GameObject enemigo = Instantiate (enemigoPrefab, spawn2.transform.position, spawn2.transform.rotation) as GameObject;
	//GameObject enemigo = Instantiate (enemigoPrefab, spawn3.transform.position, spawn3.transform.rotation) as GameObject;
	//GameObject enemigo = Instantiate (enemigoPrefab, spawn4.transform.position, spawn4.transform.rotation) as GameObject;
}
