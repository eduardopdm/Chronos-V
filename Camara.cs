using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {

	public GameObject personaje;
	public GameObject camara;
	public GameObject posCamara1;
	public GameObject posCamara2;
	public GameObject posCamara3;
	public GameObject posCamara4;

	public GameObject ragDoll;

	public bool izquierda = false;
	public bool arriba = true;
	public int speed;
	// Use this for initialization
	void Start () {
		camara.transform.position = posCamara2.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (personaje.GetComponent<PjDef> ().vivo == true) {

			this.transform.position = personaje.transform.position;

			if (Input.GetKey (KeyCode.A)) {
				izquierda = true;
			}
			if (Input.GetKey (KeyCode.D)) {
				izquierda = false;
			}
			if (arriba == true) {

				if (camara.transform.position.y < posCamara1.transform.position.y) {
					camara.transform.Translate (0, speed * Time.deltaTime, 0);
				}
				if (camara.transform.position.y > posCamara1.transform.position.y) {
				}

				if (izquierda == true) {
					if (camara.transform.position.x > posCamara1.transform.position.x) {
						camara.transform.Translate (-speed * Time.deltaTime, 0, 0);
					}
					if (camara.transform.position.x < posCamara1.transform.position.x) {

					}
				} else {
					if (camara.transform.position.x < posCamara2.transform.position.x) {
						camara.transform.Translate (speed * Time.deltaTime, 0, 0);
					}
					if (camara.transform.position.x > posCamara2.transform.position.x) {

					}
				}
			}

			if (arriba == false) {

				if (camara.transform.position.y > posCamara3.transform.position.y) {
					camara.transform.Translate (0, -speed * Time.deltaTime, 0);
				}
				if (camara.transform.position.y < posCamara3.transform.position.y) {
				}

				if (izquierda == true) {
					if (camara.transform.position.x > posCamara3.transform.position.x) {
						camara.transform.Translate (-speed * Time.deltaTime, 0, 0);
					}
					if (camara.transform.position.x < posCamara3.transform.position.x) {

					}
				} else {
					if (camara.transform.position.x < posCamara4.transform.position.x) {
						camara.transform.Translate (speed * Time.deltaTime, 0, 0);
					}
					if (camara.transform.position.x > posCamara4.transform.position.x) {
 
					}
				}
			}
		} else {
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "ColliderCamara1") {
			arriba = false;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "ColliderCamara1") {
			arriba = true;
		}
	}


}
