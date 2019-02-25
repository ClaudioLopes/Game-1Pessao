using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour, IDanificavel {

	public int vida = 50;

	Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();
	}

	void Update () {
		
	}

	public void TomaDano (int dano){
		vida -= dano;
		if(vida <= 0){
			Destroi ();
		}
	}

	public void Destroi(){
		animator.SetBool ("caindo", true);
		GetComponent<Collider> ().enabled = false;
		GetComponent<ZombieMove> ().Morre ();
	}
}
