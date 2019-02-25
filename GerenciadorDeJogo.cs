using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GerenciadorDeJogo : MonoBehaviour {

	public Text municaoText;
	public Text saudeText;

	#region Singleton

	public static GerenciadorDeJogo instancia;


	void Awake(){
		if (instancia == null) {
			instancia = this;
			DontDestroyOnLoad (gameObject);
		} else if(instancia != this){
			Destroy (gameObject);
		}
	}
	#endregion

	public void SetMunicao(int municaoNoPente, int municaoNaArma){
		municaoText.text = "Munição " + municaoNoPente.ToString () + " / " + municaoNaArma.ToString ();
	}

	public void SetSaude(int saude){
		saudeText.text = "-Saúde " + saude.ToString ();
	}
}
