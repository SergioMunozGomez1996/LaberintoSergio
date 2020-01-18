using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perseguir : MonoBehaviour
{
    public float speed = 5.0f;
    public float minDist = 1f;
    public Transform target;

    // Función de inicialización
    void Start()
    {
        // Si no tenemos un Target especificado, se perseguira al objeto con el tag Player. 
        if (target == null)
        {
            //Buscamos un objeto con el Tag Player.  
            if (GameObject.FindWithTag("Player") != null)
            {       //Si lo encontramos lo situamos como Target. 
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }

    // Se le llama en cada frame
    void Update()
    {
        //Si no tenemos un target no hacemos nada. 
        if (target == null)
            return;

        // Mira hacia el target. 
        transform.LookAt(target);

        //Recupera la distancia que hay desde el objeto al target
        float distance = Vector3.Distance(transform.localPosition, target.localPosition);

        //Nos movemos hacia el target a la velocidad indicada. 
        if (distance > minDist)
            transform.localPosition += transform.forward * speed * Time.deltaTime;
    }
}
