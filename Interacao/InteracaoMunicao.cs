using UnityEngine;
using System.Collections;

public class InteracaoMunicao : MonoBehaviour, IInteragivel {

	public int tipo;

	public void ExecutaAcao(){
		GerenciadorArma armaAtual = GameObject.FindWithTag ("Player").GetComponentInChildren<GerenciadorArma> ();
		if(armaAtual.IncrementaMunicao(tipo)){
			Destroy(gameObject);
		}
	}
}
