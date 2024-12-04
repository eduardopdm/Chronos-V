using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoCarga : MonoBehaviour {

	public int speed;
	private bool cargando;
	private bool vivo;

	public GameObject spawn;
	public GameObject personaje;
	public int distancia;
	private Vector3 vectorAlPersonaje;
	private float distanciaAlPersonaje;

	public Animator miAnimator;

	public GameObject spawnCabeza;
	public GameObject spawnPecho;
	public GameObject spawnManoD;
	public GameObject spawnEscudo;
	public GameObject cabeza;
	public GameObject pecho;
	public GameObject mano;
	public GameObject escudo;

	public GameObject ParticlePropulsion1;
	public GameObject ParticlePropulsion2;
	public GameObject ParticleVelcidad;
	public Transform SpawnPropulsion;
	public Transform SpawnVelocidad;
	// Use this for initialization
	void Start () {
		vivo = true;
		cargando = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (vivo) {
			vectorAlPersonaje = personaje.transform.position - this.transform.position;
			distanciaAlPersonaje = vectorAlPersonaje.magnitude;
			if (distanciaAlPersonaje <= distancia) {
				miAnimator.SetTrigger ("Montarse");
				vivo = false;
				Invoke ("Carga", 2);
			}
		}
		RaycastHit choque;
		if (Physics.Raycast (spawn.transform.position, this.transform.forward, out choque, 0.5f)) {
			if (choque.collider.gameObject.tag != "Player") {
				StopCarga ();
			}
		}
	}

	void FixedUpdate (){
		if (cargando) {
			transform.Translate (0, 0, speed * Time.deltaTime);
		}
	}

	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == "Player") {
			StopCarga ();
		}
	}

	void Carga (){
		GameObject ClonParticlePropusion1 =Instantiate (ParticlePropulsion1, SpawnPropulsion.transform.position, SpawnPropulsion.transform.rotation) as GameObject;
		ClonParticlePropusion1.transform.parent = this.transform;
		GameObject ClonParticlePropusion2 =Instantiate (ParticlePropulsion2, SpawnPropulsion.transform.position, SpawnPropulsion.transform.rotation) as GameObject;
		ClonParticlePropusion2.transform.parent = this.transform;
		GameObject ClonParticleVelocidad =Instantiate (ParticleVelcidad, SpawnVelocidad.transform.position, SpawnVelocidad.transform.rotation) as GameObject;
		ClonParticleVelocidad.transform.parent = this.transform;

		miAnimator.SetTrigger ("Cargar");
		cargando = true;
	}

	void StopCarga (){
		speed = 0;
		cargando = false;
		Muerte ();
	}

	void Muerte(){
		GameObject piezaCabeza = Instantiate (cabeza, spawnCabeza.transform.position, spawnCabeza.transform.rotation) as GameObject;
		Destroy (piezaCabeza, 3);
		GameObject piezaPecho = Instantiate (pecho, spawnPecho.transform.position, spawnPecho.transform.rotation) as GameObject;
		Destroy (piezaPecho, 3);
		GameObject piezaManoD = Instantiate (mano, spawnManoD.transform.position, spawnManoD.transform.rotation) as GameObject;
		Destroy (piezaManoD, 3);
		GameObject piezaEscudo = Instantiate (escudo, spawnEscudo.transform.position, spawnEscudo.transform.rotation) as GameObject;
		Destroy (piezaEscudo, 3);
		Destroy (this.gameObject);
	}
}
