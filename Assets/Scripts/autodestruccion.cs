using UnityEngine;
using System.Collections;

public class autodestruccion : MonoBehaviour {

	public float timeOut = 3.0f; //Variable que indica los segundos para la destrucción. 
	public bool detachChildren = false; //En caso de que necesitemos destruir un objeto, pero que sus hijos aún esten en el juego. 

	// se llama al crearse
	void Awake () {
		// Invocaremos al metodo de destrucción pasados los segundos indicados. 
		Invoke ("DestroyNow", timeOut);
	}

	void DestroyNow ()
	{
		if (detachChildren) { // detach the children before destroying if specified
			transform.DetachChildren ();
		}
		DestroyObject (gameObject);
	}
}
