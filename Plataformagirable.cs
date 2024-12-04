using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformagirable : MonoBehaviour {

	public GameObject pivote;
	public float speed;
	public float rotacionAcumulada = 0;
	private bool rotable = true;
	private Quaternion rotFinal;
	public int tiempoSeCae;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == "Player" && rotable == true) {
			StartCoroutine ("Accion");
		}
	}*/
	public void Activar(){
		StartCoroutine ("Accion");
	}

	public IEnumerator Accion (){
		rotable = false;
		pivote.transform.Rotate (-90, 0, 0);
		rotFinal = pivote.transform.rotation;
		pivote.transform.Rotate (90, 0, 0);

		yield return new WaitForSeconds (1);

		while (rotacionAcumulada < 90) {
			pivote.transform.Rotate (-speed * Time.deltaTime, 0, 0);
			rotacionAcumulada += speed * Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		pivote.transform.rotation = rotFinal;

		yield return new WaitForSeconds (1);

		pivote.transform.Rotate (90, 0, 0);
		rotFinal = pivote.transform.rotation;
		pivote.transform.Rotate (-90, 0, 0);

		rotacionAcumulada = 0;
		while (rotacionAcumulada < 90) {
			pivote.transform.Rotate (speed * Time.deltaTime, 0, 0);
			rotacionAcumulada += speed * Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		pivote.transform.rotation = rotFinal;
		rotacionAcumulada = 0;
		rotable = true;
	}
}
