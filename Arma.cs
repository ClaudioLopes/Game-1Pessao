using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour {

	public float alcance;
	public string nome;
	public int dano;
	public int capacidadeDoPente;
	public int capacidadeDaArma;
	public float taxaDeDisparo;

	ParticleSystem particulaDisparo;
	PiscarLuz piscaLuz;

	public bool sniper;
	public int municaoAtualNoPente;
	public int municaoAtaulNaArma;

	void Awake () {
		municaoAtualNoPente = capacidadeDoPente;
		municaoAtaulNaArma = capacidadeDaArma;
		particulaDisparo = GetComponentInChildren<ParticleSystem> ();
		piscaLuz = GetComponentInChildren<PiscarLuz> ();
	}

	public void EfeitoDisparo(){
		particulaDisparo.Play ();
		piscaLuz.Pisca ();
	}
}
