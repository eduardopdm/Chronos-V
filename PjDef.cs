using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PjDef : MonoBehaviour {

	public int speed;
	public int jump;
	private bool jumpNumber = false;
	private bool semaforoSalto;
	public bool vivo = true;

	float contadorSalto = 0;
	bool inicioContadorSalto = false;

	public GameObject spawn;
	private Rigidbody pjRig;
	private bool disparo = true;
	private bool invulnerable = false;
	private bool siendoEmpujado = false;
	public int fuerzaEmpujon;

	public bool emparentadoAlAscensor = false;

	public int cargasCambio = 3;
	public int muertesParaCambio;
	public int vida;
	private int vidaInicial;
	private float puntoVidaEnBarra;
	public float tiempo;
	private float tiempoInicial;
	private float segundoEnBarra;
	public Image vidaImagen;
	public Image tiempoImagen;
	public Image cargasImagen;

	public int dañobala;
	public int dañoTocarEnemigo;
	public int dañoBomba;
	public int dañoVapor;

	public  Animator miAnimator;

	public GameObject ParticleShoot;
	public GameObject ParticleShootHalo;
	public GameObject ParticleImpact;
	public GameObject ParticleJump;
	public GameObject ParticleJump2;
	public GameObject ParticleExplosion;
	public Transform SpawnParticleShoot;
	public Transform SpawnParticleJump;
	public GameObject ascensor;
	public GameObject ParticulaCambio;

	public GameObject[] rellenoMochila;
	public int parteMochila = 0;

	public GameObject conejo;
	public GameObject pistola;
	public Renderer rend;
	public Renderer rend2;
	public Color miColor;
	public Color semiTransparente;

	public GameObject Ragdoll;
	public GameObject Esqueleto;

	public GameObject Lluvia;

	public Scenes SceneMuete;

	void Start (){
		SceneMuete = GameObject.Find ("Canvas").GetComponent<Scenes> ();
		pjRig = this.GetComponent<Rigidbody> ();

		vidaInicial = vida;
		tiempoInicial = tiempo;
		puntoVidaEnBarra = 1f / vida;
		segundoEnBarra = 1f / tiempo;

		rend = conejo.GetComponent<Renderer> ();
		//rend2 = pistola.GetComponent<Renderer> ();
		miColor = rend.material.color;
		//semiTransparente = new Color (1f, 1f, 1f, 0.2f);
		ascensor = GameObject.FindGameObjectWithTag ("Ascensor");
	}

	void Update () {

		if (vida <= 0 || tiempo <= 0) {
			Muerte ();
		}
		if (inicioContadorSalto == true) {
			contadorSalto += Time.deltaTime;
		}

		if (vivo == true) {
			tiempo -= Time.deltaTime;
			tiempoImagen.fillAmount -= segundoEnBarra * Time.deltaTime;

			if (muertesParaCambio >= 2) {
				muertesParaCambio = 0;
				if (cargasCambio < 3) {
					if (parteMochila > 0) {
						parteMochila--;
						rellenoMochila [parteMochila].SetActive (true);
					}
					cargasCambio++;
					cargasImagen.fillAmount += 0.33f;
				}
			}

			if (Input.GetKeyDown (KeyCode.Space) && siendoEmpujado == false) {

				RaycastHit colisionSalto;
				if (Physics.Raycast (transform.position, new Vector3 (0, -1, 0), out colisionSalto, this.transform.localScale.y + 0.001f)) {
					if (colisionSalto.transform.tag == "suelo") {
						if (ascensor.GetComponent<Ascensor>().subiendo == true) {
							pjRig.velocity = new Vector3 (pjRig.velocity.x, 1.1f * jump, 0);
						} else {
							pjRig.velocity = new Vector3 (pjRig.velocity.x, 0.9f * jump, 0);
						}
						miAnimator.SetTrigger ("salto");
						inicioContadorSalto = true;
						semaforoSalto = false;
						Invoke ("CoolDownDobleSalto", 0.3f);
						GameObject ClonParticleJump = Instantiate (ParticleJump, SpawnParticleJump.position, ParticleJump.gameObject.transform.rotation) as GameObject;
						Destroy (ClonParticleJump, 2);
						return;
					}
				}
				if (jumpNumber == true && semaforoSalto == true) {
					if (ascensor.GetComponent<Ascensor>().subiendo == true) {
						pjRig.velocity = new Vector3 (pjRig.velocity.x, 1f * jump, 0);
					} else {
						pjRig.velocity = new Vector3 (pjRig.velocity.x, 0.8f * jump, 0);
					}
					jumpNumber = false;
					miAnimator.SetTrigger ("salto");
					Debug.Log ("tiempo salto : " + contadorSalto);
					contadorSalto = 0;
					inicioContadorSalto = false;
					GameObject ClonParticleJump2 = Instantiate (ParticleJump2, SpawnParticleJump.position, ParticleJump2.gameObject.transform.rotation) as GameObject;
					Destroy (ClonParticleJump2, 2);
				}
			}

			if (Input.GetKeyDown (KeyCode.Q)) {
				if (vida < vidaInicial && cargasCambio > 0) {
					ParticulasCambio ();
					vida += 2;
					tiempo -= 5;
					if (vida > vidaInicial) {
						vida = vidaInicial;
					}
					cargasImagen.fillAmount -= 0.33f;
					vidaImagen.fillAmount += 2 * puntoVidaEnBarra;
					tiempoImagen.fillAmount -= 5 * segundoEnBarra;
					cargasCambio--;
					if (parteMochila <= 3) {
						rellenoMochila [parteMochila].SetActive (false);
						parteMochila++;
					}
				}
			}

			if (Input.GetKeyDown (KeyCode.E)) {
				if (cargasCambio > 0 && vida > 2) {
					ParticulasCambio ();
					vida -= 2;
					tiempo += 5;
					if (tiempo > tiempoInicial) {
						tiempo = tiempoInicial;
					}
					cargasImagen.fillAmount -= 0.33f;
					vidaImagen.fillAmount -= 2 * puntoVidaEnBarra;
					tiempoImagen.fillAmount += 5 * segundoEnBarra;
					cargasCambio--;
					if (parteMochila <= 3) {
						rellenoMochila [parteMochila].SetActive (false);
						parteMochila++;
					}
				}
			}

			if (cargasImagen.fillAmount < 0.2) {
				cargasImagen.fillAmount = 0;
			}
			if (cargasImagen.fillAmount > 0.8) {
				cargasImagen.fillAmount = 1;
			}
			if (Input.GetKeyDown (KeyCode.P)) {
			
				if (disparo == true) {
					miAnimator.SetTrigger ("disparo");
					GameObject ClonParticleShoot = Instantiate (ParticleShoot, SpawnParticleShoot.position, SpawnParticleShoot.rotation) as GameObject;
					GameObject ClonParticleShootHalo = Instantiate (ParticleShootHalo, SpawnParticleShoot.position, SpawnParticleShoot.rotation) as GameObject;
					if (emparentadoAlAscensor == true) {
						ClonParticleShoot.transform.parent = ascensor.transform;
						ClonParticleShootHalo.transform.parent = ascensor.transform;
					}
					Destroy (ClonParticleShoot, 1);
					Destroy (ClonParticleShootHalo, 1);
					disparo = false;
					Invoke ("CoolDownDisparo", 0.5f);
					RaycastHit colisionDisparo;
					if (Physics.Raycast (spawn.transform.position, spawn.transform.forward, out colisionDisparo, 10)) {
						GameObject ClonParticleImpact = Instantiate (ParticleImpact, colisionDisparo.point, SpawnParticleShoot.rotation) as GameObject;
						Destroy (ClonParticleImpact, 1);
						if (emparentadoAlAscensor == true) {
							ClonParticleImpact.transform.parent = ascensor.transform;
						}
						GameObject ClonParticleExplosion = Instantiate (ParticleExplosion, colisionDisparo.point, SpawnParticleShoot.rotation) as GameObject;
						Destroy (ClonParticleExplosion, 1);
						if (emparentadoAlAscensor == true) {
							ClonParticleExplosion.transform.parent = ascensor.transform;
						}
						if (colisionDisparo.collider.gameObject.tag == "Enemy" || colisionDisparo.collider.gameObject.tag == "EnemyNoDMG") {
							if (colisionDisparo.collider.GetComponent<EnemigoDisparo> () != null) {
								colisionDisparo.collider.GetComponent<EnemigoDisparo> ().Daño ();
							}
							if (colisionDisparo.collider.GetComponent<EnemigoBomba> () != null) {
								colisionDisparo.collider.GetComponent<EnemigoBomba> ().Daño ();
							}
						}
						if (colisionDisparo.collider.gameObject.tag == "palanca") {
							if (colisionDisparo.collider.GetComponent<PalancaPuerta> ().semaforo == true) {
								colisionDisparo.collider.GetComponent<PalancaPuerta> ().Inicio ();
							}
						}
					} else {
						GameObject ClonParticleImpact = Instantiate (ParticleImpact, this.transform.position + (this.transform.right * 10), SpawnParticleShoot.rotation) as GameObject;
						if (emparentadoAlAscensor == true) {
							ClonParticleImpact.transform.parent = ascensor.transform;
						}
						Destroy (ClonParticleImpact, 1);
						GameObject ClonParticleExplosion = Instantiate (ParticleExplosion, this.transform.position + (this.transform.right * 10), SpawnParticleShoot.rotation) as GameObject;
						if (emparentadoAlAscensor == true) {
							ClonParticleExplosion.transform.parent = ascensor.transform;
						}
						Destroy (ClonParticleExplosion, 1);
					}
				}
			}
		}
	}

	void FixedUpdate () {
		if (vivo == true) {
			if (siendoEmpujado == false) {
				pjRig.velocity = new Vector3 (0, pjRig.velocity.y, 0);
				miAnimator.SetBool ("run", false);
				if (Input.GetKey (KeyCode.A)) {
					transform.eulerAngles = new Vector3 (0, 180, 0);
					miAnimator.SetBool ("run", true);
					this.transform.Translate (-this.transform.right * speed * Time.deltaTime);
				}
				if (Input.GetKey (KeyCode.D)) {
					transform.eulerAngles = new Vector3 (0, 0, 0);
					miAnimator.SetBool ("run", true);
					this.transform.Translate (this.transform.right * speed * Time.deltaTime);
				}
			}
		} else {
			pjRig.velocity = new Vector3 (0, pjRig.velocity.y, 0);
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "emptyAscensor") {
			emparentadoAlAscensor = true;
			Lluvia.SetActive (false);
		}
		if (other.gameObject.tag == "alcantarillas") {
			Lluvia.SetActive (false);
		}

		if (other.gameObject.tag == "muerte") {
			vida = 0;
			vidaImagen.fillAmount = 0;
		}

		if (other.gameObject.tag == "bala" && invulnerable == false) {
			InvulnerableOn ();
			Invoke ("InvulnerableOff", 1);
			Destroy (other.gameObject);
			vida -= 2;
			vidaImagen.fillAmount -= 2 * puntoVidaEnBarra;
		}

		if (other.gameObject.tag == "vapor") {
			if (invulnerable == false) {
				InvulnerableOn ();
				Invoke ("InvulnerableOff", 1);
				vida -= 3;
				vidaImagen.fillAmount -= 3 * puntoVidaEnBarra;
			}
			Empujon (other.transform.position);
		}

		if (other.gameObject.tag == "Vial") {
			Destroy (other.gameObject);
			vida += 3;
			if (vida > vidaInicial) {
				vida = vidaInicial;
			}
			vidaImagen.fillAmount += 3 * puntoVidaEnBarra;
			tiempo += 10;
			if (tiempo > tiempoInicial) {
				tiempo = tiempoInicial;
			}
			tiempoImagen.fillAmount += 10 * segundoEnBarra;
		}

		if (other.gameObject.tag == "ParticulaPoder") {
			muertesParaCambio++;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "emptyAscensor") {
			this.transform.parent = null;
			emparentadoAlAscensor = false;
			Lluvia.SetActive (true);
		}
		if (other.gameObject.tag == "alcantarillas") {
			Lluvia.SetActive (true);
		}

	}
		
	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == "Enemy") {
			if (invulnerable == false) {
				InvulnerableOn ();
				Invoke ("InvulnerableOff", 1);
				vida -= 3;
				vidaImagen.fillAmount -= 3 * puntoVidaEnBarra;
			}
			Empujon (other.transform.position);
		}
		if (other.gameObject.tag == "EnemyInmortal") {
			vida = 0;
			vidaImagen.fillAmount = 0;
			Empujon (other.transform.position);
		}

		if (other.gameObject.tag == "suelo") {
			RaycastHit colisionSalto;
			if (Physics.Raycast (transform.position, new Vector3 (0, -1, 0), out colisionSalto, this.transform.localScale.y + 0.001f)) {
				if (colisionSalto.transform.tag == "suelo") {
					if (other.collider.GetComponent<Plataformagirable> () != null) {
						other.collider.GetComponent<Plataformagirable> ().Activar ();
					}
				}
			}
		}
	}

	void OnCollisionStay (Collision other){
		if (other.gameObject.tag == "suelo") {
			RaycastHit colisionSalto;
			if (Physics.Raycast (transform.position, new Vector3 (0, -1, 0), out colisionSalto, this.transform.localScale.y + 0.001f)) {
				if (colisionSalto.transform.tag == "suelo") {
					miAnimator.SetBool("aire", false);
					jumpNumber = true;
				}
			}
		}
	}

	void OnCollisionExit (Collision other){
		if (other.gameObject.tag == "suelo") {
			//miAnimator.SetBool ("aire", true);
			RaycastHit colisionSalto;
			if (Physics.Raycast (transform.position, new Vector3 (0, -1, 0), out colisionSalto, this.transform.localScale.y + 0.001f) == false) {
				miAnimator.SetBool ("aire", true);
			} else {
				if (colisionSalto.transform.tag != "suelo") {
					miAnimator.SetBool ("aire", true);
				}
			}
		}
	}

	public void Empujon ( Vector3 pos ){
		Debug.Log ("Empujon");
		siendoEmpujado = true;
		Invoke ("CoolDownEmpujon", 0.3f);
		if (this.transform.position.x < pos.x) {
			pjRig.AddForce (Vector3.left * fuerzaEmpujon, ForceMode.Impulse);
		} else {
			pjRig.AddForce (Vector3.right * fuerzaEmpujon, ForceMode.Impulse);
		}
		if (this.transform.position.y < pos.y) {
			pjRig.AddForce (Vector3.down * fuerzaEmpujon * 0.5f, ForceMode.Impulse);
		} else {
			pjRig.AddForce (Vector3.up * fuerzaEmpujon * 0.5f, ForceMode.Impulse);
		}
	}

	void InvulnerableOn (){
		invulnerable = true;
		StartCoroutine ("CambioColor");
	}

	void InvulnerableOff (){
		invulnerable = false;
	}

	void CoolDownDisparo (){
		
		disparo = true;
	}

	void CoolDownEmpujon(){
		siendoEmpujado = false;
	}

	void CoolDownDobleSalto (){
		semaforoSalto = true;
	}

	IEnumerator CambioColor(){
		Debug.Log ("Brubrubru");
		rend.material.color = semiTransparente;
		yield return new WaitForSeconds (0.2f);
		rend.material.color = miColor;
		yield return new WaitForSeconds (0.2f);
		rend.material.color = semiTransparente;
		yield return new WaitForSeconds (0.2f);
		rend.material.color = miColor;
		yield return new WaitForSeconds (0.2f);
		rend.material.color = semiTransparente;
		yield return new WaitForSeconds (0.2f);
		rend.material.color = miColor;
	}
	void Muerte(){
		vivo = false;
		Ragdoll.SetActive (true);
		Ragdoll.GetComponent<Rigidbody> ().AddForce (Ragdoll.transform.forward * -100, ForceMode.Impulse);
		conejo.SetActive (false);
		Esqueleto.SetActive (false);
		SceneMuete.Muerte1 ();
	}
	void ParticulasCambio(){
		GameObject ClonParticleCambio = Instantiate (ParticulaCambio, this.transform.position + Vector3.up*0.45f, this.transform.rotation) as GameObject;
		ClonParticleCambio.transform.parent = this.transform;
		Destroy (ClonParticleCambio, 1);
	}
}
