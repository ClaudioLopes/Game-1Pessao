using UnityEngine;
using System.Collections;

public class Configuracoes : MonoBehaviour {

	public static Configuracoes instancia; 	

	public float duracaoBala;
	public float distanciaBuracoBala;
	public float forcaImpacto;
	public float zoomArma;
	public float zoomPadrao;
	public float forcaGranada;
	public int quantidadeDeGranadas;
	public float distanciaInteracao;
	public float tempoDePercepcao;

	void Awake(){
		if (instancia == null) {
			instancia = this;
			DontDestroyOnLoad (gameObject);
		} else if(instancia != this){
			Destroy (gameObject);
		}
	}

	void Start () {
	
	}

	void Update () {
	
	}
}
