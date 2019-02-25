using UnityEngine;
using System.Collections;

public class GerenciadorArma : MonoBehaviour {
	#region Variaveis

	public GameObject marcaBala;
	public Transform armaPrimaria;
	public Transform armaSecundaria;
	public GameObject granada;

	GameObject armaAtual;
	Camera cameraFPS;
	bool atirando = false;
	bool mirando = false;
	Animator animator;
	Arma arma;
	int granadas;
	Configuracoes configuracao;
	int tipoArmaSelecionada;
	float proximoTiro = 0.0f;

	#endregion

	#region metodo Unity

	void Start () {
		TrocaDeArma (1);
		armaAtual = transform.GetChild (1).GetChild(0).gameObject;
		cameraFPS = transform.parent.gameObject.GetComponent<Camera> ();
		animator = GetComponent<Animator> ();
		configuracao = Configuracoes.instancia;
		granadas = configuracao.quantidadeDeGranadas;
	}

	void Update () {
		//ATIRAR
		if (Input.GetMouseButton(0) && arma.municaoAtualNoPente > 0  && Time.time > proximoTiro) {
			proximoTiro = Time.time + 1f/arma.taxaDeDisparo;
			Atira ();
		}

		//RECARREGAR
		if (Input.GetKeyDown ("r")) {
			Recarrega ();
		}

		//TROCA DE ARMA	
		if(Input.GetKeyDown("1") || Input.GetAxis("Mouse ScrollWheel") < 0f){
			TrocaDeArma (1);
		}

		if(Input.GetKeyDown("2") || Input.GetAxis("Mouse ScrollWheel") > 0f){
			TrocaDeArma (2);
		}

		//MIRA
		if (Input.GetMouseButtonDown (1)) {
			mirando = true;
			animator.SetBool ("mirando", mirando);
			cameraFPS.fieldOfView = configuracao.zoomArma;
		}
		if (Input.GetMouseButtonUp (1)) {
			mirando = false;
			animator.SetBool ("mirando", mirando);
			cameraFPS.fieldOfView = configuracao.zoomPadrao;
		}

		//GRANADA
		if (Input.GetKeyDown ("g") && granadas > 0) {
			AtiraGranada ();
		}

		//INTERAGIR
		if (Input.GetKeyDown ("e")) {
			Interagir ();
		}
	}

	#endregion

	void Atira(){
		arma.EfeitoDisparo ();

		arma.municaoAtualNoPente--;
		AtualizaMunicao ();

		RaycastHit objetoAcertado;
		float distancia = arma.alcance;

		if(Physics.Raycast(cameraFPS.transform.position, cameraFPS.transform.forward, out objetoAcertado, distancia)){
			GameObject buracoBala = (GameObject)Instantiate (marcaBala, objetoAcertado.point + objetoAcertado.normal * configuracao.distanciaBuracoBala, Quaternion.FromToRotation(Vector3.up, objetoAcertado.normal));
			Destroy (buracoBala, configuracao.duracaoBala);
			buracoBala.transform.parent = objetoAcertado.transform;
			Rigidbody corpoAtingido = objetoAcertado.transform.gameObject.GetComponent<Rigidbody> ();
			if(corpoAtingido != null){
				corpoAtingido.AddForce (-objetoAcertado.normal * arma.dano * configuracao.forcaImpacto);
			}

			IDanificavel danificavel = objetoAcertado.transform.gameObject.GetComponent<IDanificavel>();
			if (danificavel != null) {
				danificavel.TomaDano (arma.dano);
			}

		}
		if(arma.municaoAtualNoPente == 0){
			Recarrega ();
		}
	}

	void TrocaDeArma(int armaSelecionada){
		tipoArmaSelecionada = armaSelecionada;
		if(armaSelecionada == 1){
			armaAtual = armaPrimaria.GetChild(0).gameObject;
			armaPrimaria.gameObject.SetActive (true);
			armaSecundaria.gameObject.SetActive (false);
		}
		if (armaSelecionada == 2) {
			armaAtual = armaSecundaria.GetChild(0).gameObject;
			armaPrimaria.gameObject.SetActive (false);
			armaSecundaria.gameObject.SetActive (true);
		}
		arma = armaAtual.GetComponent<Arma> ();
		AtualizaMunicao ();
	}

	void Recarrega(){
		if (arma.municaoAtualNoPente == arma.capacidadeDoPente || arma.municaoAtaulNaArma == 0) {
			//FAZ BARULHO DE ARMA VAZIA ("CLICK")
		} else {
			int balasNecessarias = arma.capacidadeDoPente - arma.municaoAtualNoPente;
			arma.municaoAtaulNaArma -= balasNecessarias;
			arma.municaoAtualNoPente += balasNecessarias;
			animator.SetBool ("recarregando", true);
		}
		AtualizaMunicao ();
	}

	void AtualizaMunicao(){
		GerenciadorDeJogo.instancia.SetMunicao (arma.municaoAtualNoPente, arma.municaoAtaulNaArma);
	}

	void AtiraGranada(){
		GameObject granadaLancada = (GameObject)Instantiate (granada, cameraFPS.transform.position, Quaternion.identity);
		Vector3 forca = transform.forward * configuracao.forcaGranada;
		forca.y += 10f;
		granadaLancada.GetComponent<Rigidbody> ().AddForce (forca);
		granadas--;
	}

	void Interagir (){
		RaycastHit objetoAcertado;
		float distancia = configuracao.distanciaInteracao;

		if(Physics.Raycast(cameraFPS.transform.position, cameraFPS.transform.forward, out objetoAcertado, distancia)){
			IInteragivel interagivel = objetoAcertado.transform.gameObject.GetComponent<IInteragivel> ();
			if (interagivel != null) {
				interagivel.ExecutaAcao ();
			}
		}
	}

	public bool IncrementaMunicao(int tipo){
		if (tipo == tipoArmaSelecionada) {
			if (arma.municaoAtaulNaArma != arma.capacidadeDaArma || arma.municaoAtualNoPente != arma.capacidadeDoPente) {
				arma.municaoAtaulNaArma = arma.capacidadeDaArma; 
				arma.municaoAtualNoPente = arma.capacidadeDoPente;
				AtualizaMunicao ();
				return true;
			}
		}
		return false;
	}
}
