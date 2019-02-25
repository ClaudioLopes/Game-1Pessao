using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMove : MonoBehaviour {

	public float velocidade = 2.0f;

	NavMeshAgent agent;
	Animator animacao;
	float fov = 60f;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		animacao = GetComponent<Animator> ();
		agent.speed = velocidade;
		//animacao.SetBool ("movimentando", true);
	}

	public void Morre(){
		agent.isStopped = true;
		enabled = false;
	}

	public void VerificaJogadorVisivel(Vector3 playerTransform){
		if (JogadorMuitoProximi(playerTransform)) {
			SegueJogador (playerTransform);
		} else {
			if (JogadorCampoVisao (playerTransform) && !JogadorNaoEstaEscondido (playerTransform)) {
				SegueJogador (playerTransform);
			}
		}
	}

	bool JogadorMuitoProximi(Vector3 posicaoJogador){
		if (Vector3.Distance (posicaoJogador, transform.position) < 3f) {
			return true;
		}
		return false;
	}

	bool JogadorCampoVisao(Vector3 posicaoJogador){
		Vector3 direcaoJogador = posicaoJogador - transform.position;
		float angulo = Vector3.Angle (direcaoJogador, transform.forward);

		if (angulo >= -fov / 2 && angulo <= fov / 2) {
			return true;
		}
		return false;
	}

	bool JogadorNaoEstaEscondido(Vector3 posicaoJogador){
		Vector3 direcaoJogador = posicaoJogador - transform.position;
		RaycastHit objetoAcertado;
		int camada = ~LayerMask.GetMask ("Inimigos");

		if (Physics.Raycast (transform.position, direcaoJogador, out objetoAcertado, camada)) {
			if (objetoAcertado.transform.tag == "Player") {
				return true;
			}
		}
		return false;
	}

	void SegueJogador(Vector3 posicaoJogador){
		agent.destination = posicaoJogador;
		animacao.SetBool ("movimentando", true);
	}
}
