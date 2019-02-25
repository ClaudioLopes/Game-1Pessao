using UnityEngine;
using System.Collections;

public interface IInteragivel{

	void ExecutaAcao ();
}


public interface IDanificavel{

	void TomaDano (int dano);
	void Destroi();

}