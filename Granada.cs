using UnityEngine;
using System.Collections;

public class Granada : MonoBehaviour {

	public float tempoAteExplosao;
	public GameObject particulaExplosao;
	public float raio;
	public float forcaExplosao;

	float cronometro = 0f;

	void Start () {
		
	}
	
	void Update () {
		cronometro += Time.deltaTime;
		if (cronometro >= tempoAteExplosao) {
			Instantiate (particulaExplosao, transform.position, Quaternion.identity);

			Collider[] objetosAfetados = Physics.OverlapSphere (transform.position, raio);
			foreach(Collider objeto in objetosAfetados){
				Rigidbody corpo = objeto.GetComponent<Rigidbody> ();
				if(corpo){
					corpo.AddExplosionForce (forcaExplosao, transform.position, raio);
				}
			}

			Destroy (gameObject);
		}
	}
}
