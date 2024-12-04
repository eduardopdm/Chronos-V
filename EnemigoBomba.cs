using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBomba : MonoBehaviour {

	public Animator myAnimator;
	public int vida = 2;
	public float speed;
	private bool movimiento;
	private bool detectado;
	public GameObject posicion1;
	public GameObject posicion2;

	public GameObject personaje;
	private bool personajeCerca;
	public int distancia;
	public int distanciaExplosion;
	private Vector3 vectorAlPersonaje;
	private float distanciaAlPersonaje;
	public bool vivo = true;

	public GameObject prefExplosion;
	public GameObject prefchispas;
	public GameObject prefParticulas;

	public GameObject armadura;
	public Renderer rend;
	public Color miColor;
	public Color azul;

	// Use this for initialization
	void Start () {
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
			myAnimator.SetBool ("Correr", true);
		} else {
			personajeCerca = false;
			myAnimator.SetBool ("Correr", false);
		}

		if (vida == 0) {
			Muerte ();
		}
	}

	void FixedUpdate (){
		if (detectado == true) {
			transform.Translate (0, 0, 3.5f * speed * Time.deltaTime);

			if (vivo == true) {
				if (personaje.transform.position.x > this.transform.position.x) {
					this.transform.rotation = Quaternion.Euler (0, 90, 0);
				} else {
					this.transform.rotation = Quaternion.Euler (0, -90, 0);
				}
			}

			if (distanciaAlPersonaje <= distanciaExplosion) {
				speed = 0;
				GameObject chispas = Instantiate (prefchispas, this.transform.position, this.transform.rotation);
				chispas.transform.parent = this.transform;
				myAnimator.SetTrigger ("Explosion");
				vivo = false;
				this.transform.rotation = Quaternion.Euler (0, 180, 0);
				Invoke ("Explosion", 1.5f);
			}
		}
	}

	IEnumerator Accion (){
		while (true && vivo == true) {
			if (personajeCerca == true) {
				detectado = true;
			}

			if (personajeCerca == false) {
				detectado = false;
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

	void Explosion (){
		GameObject explosion = Instantiate (prefExplosion, this.transform.position, this.transform.rotation);
		Destroy (explosion, 1.5f);
		if (distanciaAlPersonaje <= 5) {
			personaje.GetComponent<PjDef> ().Empujon (this.transform.position);
			personaje.GetComponent<PjDef> ().vida -= 5;
			personaje.GetComponent<PjDef> ().vidaImagen.fillAmount -= 0.5f;
		}
		Destroy (this.gameObject);
	}

	public void Muerte(){
		GameObject particulasPoder =Instantiate (prefParticulas, this.transform.position,Quaternion.identity) as GameObject;
		Destroy (particulasPoder, 5);
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
