using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChorroVapor : MonoBehaviour {

	public GameObject vapor;
	public ParticleSystem ps;
	public int delayInicio = 0;
	public int tiempoConVapor;
	public int tiempoSinVapor;
	// Use this for initialization
	void Start () {
		Invoke ("InicioVapor", delayInicio);
	}
	
	// Update is called once per frame
	IEnumerator SaleVapor (){
		while (true) {
			vapor.gameObject.SetActive (true);
			ps.Play ();
			yield return new WaitForSeconds (tiempoConVapor);
			vapor.gameObject.SetActive (false);
			ps.Stop ();
			yield return new WaitForSeconds (tiempoSinVapor);
		}
	}

	void InicioVapor (){
		StartCoroutine ("SaleVapor");
	}
}
