using UnityEngine;
using System.Collections;

public class AcaoBotao : MonoBehaviour, IInteragivel {

	public void ExecutaAcao(){
		GetComponent<Animator> ().SetBool ("apertar", true);
		print ("");
	}
}
