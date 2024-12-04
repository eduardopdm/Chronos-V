using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaPuerta : MonoBehaviour {

	public GameObject puerta;
	public GameObject palanca;
	private Vector3 puertaCerrada;
	public GameObject puertaAbierta;
	public int speedArriba;
	public int speedAbajo;
	public int tiempo;

	public bool semaforo;

	// Use this for initialization
	void Start () {
		puertaCerrada = puerta.transform.position;
		semaforo = true;
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player" && semaforo == true) {
			Inicio ();
		}
	}

	IEnumerator PuertaArriba (){
		while (puerta.transform.position.y < puertaAbierta.transform.position.y) {
			puerta.transform.Translate (0, speedArriba* Time.deltaTime, 0);
			yield return new WaitForFixedUpdate ();
		}
		yield return new WaitForSeconds (tiempo);
		while (puerta.transform.position.y > puertaCerrada.y) {
			puerta.transform.Translate (0, -speedAbajo* Time.deltaTime, 0);
			yield return new WaitForFixedUpdate ();
		}
		palanca.transform.Rotate (0, 0, -40);
		semaforo = true;
	}

	public void Inicio (){
		palanca.transform.Rotate (0, 0, 40);
		semaforo = false;
		StartCoroutine ("PuertaArriba");
	}
}
