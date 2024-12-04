using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDisparo : MonoBehaviour {

	public int speed;
	private bool movimiento;
	public GameObject posicion1;
	public GameObject posicion2;

	public GameObject personaje;
	private bool personajeCerca;
	public int distancia;
	private Vector3 vectorAlPersonaje;
	private float distanciaAlPersonaje;

	public int vida;

	public GameObject balaPref;
	public GameObject spawnBala;
	public int speedBala;

	public Animator miAnimator;

	public GameObject ParticulaMuerte1;
	public GameObject ParticulaMuerte2;

	public GameObject ascensor;

	public GameObject spawnCabeza;
	public GameObject spawnPecho;
	public GameObject spawnManoD;
	public GameObject spawnManoI;
	public GameObject cabeza;
	public GameObject pecho;
	public GameObject manoD;
	public GameObject manoI;

	public GameObject armadura;
	public Renderer rend;
	public Color miColor;
	public Color azul;


	// Use this for initialization
	void Start () {
		personaje = GameObject.FindGameObjectWithTag ("Player");
		ascensor = GameObject.FindGameObjectWithTag ("Ascensor");
		movimiento = false;
		personajeCerca = false;
		StartCoroutine ("Accion");
		rend = armadura.GetComponent<Renderer> ();
		miColor = rend.material.color;
		azul = new Color (0.176f, 0.471f, 1);
	}
	
	// Update is called once per frame
	void Update () {
		vectorAlPersonaje = personaje.transform.position - this.transform.position;
		distanciaAlPersonaje = vectorAlPersonaje.magnitude;
		if (distanciaAlPersonaje <= distancia) {
			personajeCerca = true;
			miAnimator.SetBool ("Disparo", true);
		} else {
			personajeCerca = false;
			miAnimator.SetBool ("Disparo", false);
		}

		if (vida == 0) {
			Muerte ();
		}
	}

	IEnumerator Accion (){
		while (true) {
			if (personajeCerca == true) {
				if (personaje.transform.position.x > this.transform.position.x) {
					this.transform.rotation = Quaternion.Euler (0, 90, 0);
				} else {
					this.transform.rotation = Quaternion.Euler (0, -90, 0);
				}
				yield return new WaitForSeconds (0.7f);
				if (personaje.transform.position.x > this.transform.position.x) {
					this.transform.rotation = Quaternion.Euler (0, 90, 0);
				} else {
					this.transform.rotation = Quaternion.Euler (0, -90, 0);
				}
				Shoot ();
				yield return new WaitForSeconds (0.8f);
			}

			if (personajeCerca == false) {
				
				transform.Translate (0, 0, 1 * speed * Time.deltaTime);

				if (movimiento == false) {
					this.transform.rotation = Quaternion.Euler (0, -90, 0);
					if (Vector3.Distance (posicion1.transform.position, this.transform.position) <= 0.2f) {
						movimiento = true;
					}
				} else {
					this.transform.rotation = Quaternion.Euler (0, 90, 0);
					if (Vector3.Distance (posicion2.transform.position, this.transform.position) <= 0.2f) {
						movimiento = false;
					}
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}

	void Shoot(){
		GameObject bala = Instantiate (balaPref, spawnBala.transform.position, this.transform.rotation) as GameObject;
		bala.transform.parent = ascensor.transform;
		bala.GetComponent<Rigidbody> ().AddForce (vectorAlPersonaje.normalized * speedBala, ForceMode.Impulse);
		Destroy (bala, 5);
	}

	public void Muerte(){
		GameObject ClonParticleMuerte1 =Instantiate (ParticulaMuerte1, this.transform.position,Quaternion.identity) as GameObject;
		GameObject ClonParticleMuerte2 =Instantiate (ParticulaMuerte2, this.transform.position,Quaternion.identity) as GameObject;

		ClonParticleMuerte1.transform.parent = ascensor.transform;
		ClonParticleMuerte2.transform.parent = ascensor.transform;
		Destroy (ClonParticleMuerte1, 1);
		Destroy (ClonParticleMuerte2, 5);
		GameObject piezaCabeza = Instantiate (cabeza, spawnCabeza.transform.position, spawnCabeza.transform.rotation) as GameObject;
		Destroy (piezaCabeza, 2);
		GameObject piezaPecho = Instantiate (pecho, spawnPecho.transform.position, spawnPecho.transform.rotation) as GameObject;
		Destroy (piezaPecho, 2);
		GameObject piezaManoD = Instantiate (manoD, spawnManoD.transform.position, spawnManoD.transform.rotation) as GameObject;
		Destroy (piezaManoD, 2);
		GameObject piezaManoI = Instantiate (manoI, spawnManoI.transform.position, spawnManoI.transform.rotation) as GameObject;
		Destroy (piezaManoI, 2);
		Destroy (this.gameObject);
		//personaje.GetComponent<PjDef> ().muertesParaCambio++;
	}

	public void Daño (){
		vida--;
		StartCoroutine ("CambioColor");
	}

	IEnumerator CambioColor(){
		rend.material.color = azul;
		yield return new WaitForSeconds (0.3f);
		rend.material.color = miColor;
	}
		
}
