using UnityEngine;
using System.Collections;

public class PiscarLuz : MonoBehaviour {

	public float tempoPiscada = 0.20f;
	Light luz;

	public  void Start(){
		luz = GetComponent<Light> ();
	}

	public void Pisca(){
		StartCoroutine (PiscaCoroutine());
	}

	IEnumerator PiscaCoroutine(){
		luz.enabled = true;
		yield return new WaitForSeconds (tempoPiscada);
		luz.enabled = false;
	}
}
