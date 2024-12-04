using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {
	public Image Fondo;
	public Image GameOver;
	public Image Botton;

	// Use this for initialization
	void Start () {
		Fondo.gameObject.SetActive (false);
		GameOver.gameObject.SetActive (false);
		Botton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Muerte1(){
		StartCoroutine ("ImagenesGameOver");
	}
	IEnumerator ImagenesGameOver(){
		yield return new WaitForSeconds (1);
		Fondo.gameObject.SetActive (true);
		yield return new WaitForSeconds (1);
		GameOver.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		Botton.gameObject.SetActive (true);

	}

	public void Nivel1 (){
		SceneManager.LoadScene ("Escena1");
	}
}
