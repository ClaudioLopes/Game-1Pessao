using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercepcaoPlayer : MonoBehaviour {

	int camadaInimigo;

	float raioPercepcao = 30;

	void Start () {

		InvokeRepeating ("Notificacao", Configuracoes.instancia.tempoDePercepcao, Configuracoes.instancia.tempoDePercepcao);
		camadaInimigo = LayerMask.GetMask ("Inimigos");

	}

	void Notificacao(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, raioPercepcao, camadaInimigo);
		foreach (Collider collider in colliders) {
			collider.gameObject.GetComponent<ZombieMove> ().VerificaJogadorVisivel (transform.position);
		}
	}
}
