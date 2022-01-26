using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public float currenttimeLimit = 0f;
    [Header("Time")]
    [Tooltip("Tiempo limite para recoger monedas")]
    public float timeLimit = 200f;
    [Header("UI Texts")]
    [Tooltip("Texto en el que se explica el objetivo del juego")]
    public Text startText;
    [Tooltip("Texto que indica el tiempo que queda para acabar la partida")]
    public Text timeText;
    [Tooltip("Texto en el que se indica las monedas que el jugador ha conseguido")]
    public Text coinText;
    [Tooltip("Texto de la pantalla de resultados que indica monedas finales recogidas")]
    public Text finalCoinText;
    [HideInInspector]
    public int coinCount;
    [Header("Results")]
    [Tooltip("Canvas que muestra los resultados de la partida")]
    public GameObject resultsCanvas;
    [HideInInspector]
    public bool gameStarted = false;
    [HideInInspector]
    public bool gameJustStarted = false;


    //SINGLETON
    public static GameManager instance;
    private void Awake()
    {
        //si no hay ninguna instancia de este script. este es el script correcto. si hay otro destruye este
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //configuracion inicial para evitar problemas de objetos activos o con otros valores al comenzar
        resultsCanvas.SetActive(false);
        currenttimeLimit = timeLimit;
        timeText.text = currenttimeLimit.ToString();
        coinCount = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameStarted = false;
        //empieza la partida llamando a la corrutina
        StartCoroutine(StartingGame());
    }

    // Update is called once per frame
    void Update()
    {
        //actualiza el contador de monedas de la UI
        coinText.text = coinCount.ToString();
        finalCoinText.text = coinCount.ToString();

        //si el tiempo limite es 0 o por debajo entonces muestra los resultados
        if (currenttimeLimit <= 0)
        {
            resultsCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    IEnumerator StartingGame()
    {
        //espera un poco
        yield return new WaitForSeconds(0.3f);
        //enseña al jugador el objetivo y le da tiempo a leer antes de desactivar de nuevo el texto
        startText.enabled = true;
        yield return new WaitForSeconds(5f);
        startText.enabled = false;
        //comienza el juego y su cuenta atras
        StartCoroutine(Countdown());
        gameJustStarted = true;
        yield return null;
    }

    IEnumerator Countdown()
    {
        //resta cada segundo el valor del timelimit
        while (currenttimeLimit > 0)
        {
            currenttimeLimit = currenttimeLimit - 1f;
            timeText.text = currenttimeLimit.ToString();
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    public void RestartLevel()
    {
        //para los botones de la UI. se llama desde uno de ellos al hacer click
        //recarga la escena para volver a jugar
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        //para los botones de la UI. se llama desde uno de ellos al hacer click
        //sale del juego
        Application.Quit();
    }
}
