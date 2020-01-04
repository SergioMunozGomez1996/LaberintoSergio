using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System; // Importar esto para usar la clase Array
using UnityEngine.SceneManagement; // Importar esto para usar el manejador de escenas

public class PlayerController : MonoBehaviour {

	public float velocidad;
    public Text countText;
    public Text winText;
    // Imágenes de las vidas
    public GameObject[] vidasImages;

    private Rigidbody rb;
    // Contador del número de coleccionables que ha conseguido
    private int contador;
    // Contador de vidas que dispone el jugador
    private int contadorVidas;
    // Número de boo que tiene que coleccionar el jugador para pasar de nivel
    private int booTotal;
    // Nombre de la escena actual
    private string sceneName;

    // Posición original de la bola y rotación original del tablero (laberinto)
    private Vector3 originalBallPosition;
    private Quaternion originalMazeRotation;


    void Start() 
	{
		rb = GetComponent<Rigidbody>();
        // Establecemos el número de coleccionables iniciales
        contador = 0;
        // Establecemos el número de vidas a 3
        contadorVidas = 3;
        // Establecemos el número de boo de cada nivel como el número de boo que tiene que conseguir el jugador
        booTotal = GameObject.FindGameObjectsWithTag("boo").Length;
        // Almacenamos la posición original de la pelota y la rotación original del tablero
        originalBallPosition = gameObject.transform.position;
        originalMazeRotation = GameObject.FindWithTag("laberinto").transform.rotation;

        // Obtenemos el nombre de la escena activa
        sceneName = SceneManager.GetActiveScene().name;

        SetCountText();
        winText.text = "";
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        float posH = Input.GetAxis("Horizontal");
        float posV = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(posH, 0.0f, posV);

       	rb.AddForce(movimiento * velocidad);
	}

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag ("boo")) {
			other.gameObject.SetActive (false);
			contador = contador + 1;
			SetCountText ();

		}else if (other.gameObject.CompareTag("fondo"))
        {
            contadorVidas = contadorVidas - 1;

            deleteVidas();

            if (contadorVidas == 0)
            {
                winText.text = "Perdiste";
                Invoke("QuitGame", 1.5f);
            }
            else
            {
                // Si le quedan vidas, volvemos a colocar la pelota en su posición original y el tablero a su rotación original
                gameObject.transform.position = originalBallPosition;
                GameObject.FindWithTag("laberinto").transform.rotation = originalMazeRotation;
            }
            
        }

    }

    void SetCountText()
    {
            countText.text = "Contador: " + contador.ToString();
            if (contador >= booTotal)
            {
            switch (sceneName) {
                // Cargamos el laberinto medio
                case "laberintoFacil":
                    winText.text = "Preparate para el nivel medio";
                    Invoke("LoadSceneFacil", 1.5f);
                    break;

                // Cargamos el laberinto dificil
                case "laberintoMedio":
                    winText.text = "Ganaste";
                    Invoke("QuitGame", 1.5f);
                    break;
            }				
            }
    }

    void LoadSceneFacil()
    {
        SceneManager.LoadScene("laberintoMedio");
    }

	void QuitGame()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
		Application.OpenURL(webplayerQuitURL);
		#else
		Application.Quit();
		#endif
	}

    // Método que eliminará uno de los iconos de vidas de la esquina superior derecha.
    void deleteVidas()
    {
        vidasImages[vidasImages.Length - 1].SetActive(false);
        Array.Resize(ref vidasImages, vidasImages.Length - 1);
    }

}