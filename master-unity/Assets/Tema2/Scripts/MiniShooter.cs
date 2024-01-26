using TMPro;
using UnityEngine;

public class MiniShooter : MonoBehaviour
{
    //elementos de la UI
    [SerializeField] private TextMeshProUGUI textoFinPartida;
    [SerializeField] private TextMeshProUGUI textoPuntuacion;

    //personaje principal
    [SerializeField] private Transform personajePrincipal;

    //configuráción de juego
    [SerializeField] private float velocidadEnemigos = 5.0f;
    [SerializeField] private float velocidadPersonaje = 20.0f;
    [SerializeField] private int ladoZonaRespawn = 40;
    [SerializeField] private float sensibilidadRaton = 10f;
    [SerializeField] private float limiteRotacionVertical = 45.0f; // límite de rotación vertical
    [SerializeField] private Color colorOro;
    [SerializeField] private Color colorPlata;
    [SerializeField] private Color colorBronce; 
    [SerializeField] private Vector3[] posicionesCamara;

    //variables de control
    private int puntuacionJugador;
    private int posicionActual;
    public static MiniShooter instance;

    private void Awake()
    {
        instance = this;
        Cursor.visible = false;                     // ocultamos el cursor       
        Cursor.lockState = CursorLockMode.Locked;   // bloqueamos el cursor en el centro de la pantalla    
    }

    void Start()
    {
        textoFinPartida.enabled = false;
        puntuacionJugador = 0;
        posicionActual = 0;
    }

    void Update()
    {
        //cambio de cámara
        if (posicionesCamara.Length > 0 && Input.GetKeyDown(KeyCode.C))
        {
            Camera.main.transform.localPosition = posicionesCamara[++posicionActual % posicionesCamara.Length];
        }
    }

    public void FinPartida()
    {
        textoFinPartida.enabled = true;
        Time.timeScale = 0;
    }
    
    //---------------------------------------------
    //------------------ EVENTOS ------------------
    //---------------------------------------------
    private void OnEnable()
    {
        // se suscribe a los eventos de los enemigos
        EnemigoShooter.enemigoImpactado += ActualizarPuntuacion;
        MonedaShooter.monedaRecogida += ActualizarPuntuacion;
    }
    private void OnDisable()
    {
        // se desuscribe a los eventos de los enemigos
        EnemigoShooter.enemigoImpactado -= ActualizarPuntuacion;
        MonedaShooter.monedaRecogida -= ActualizarPuntuacion;
    }

    public void ActualizarPuntuacion(int puntos)
    {
        puntuacionJugador += puntos;
        textoPuntuacion.text = puntuacionJugador.ToString();
    }

    //---------------------------------------------
    //------------------ GETTERS ------------------
    //---------------------------------------------

    public Transform PersonajePrincipal
    {
        get { return personajePrincipal.transform; }
    }

    public float VelocidadEnemigos
    {
        get { return velocidadEnemigos; }
    }

    public int LadoZonaRespawn
    {
        get { return ladoZonaRespawn; }
    }

    public float VelocidadPersonaje
    {
        get { return velocidadPersonaje; }
    }

    public float SensibilidadRaton
    {
        get { return sensibilidadRaton; }
    }

    public float LimiteRotacionVertical
    {
        get { return limiteRotacionVertical; }
    }

    public Color ColorOro
    {
        get { return colorOro; }
    }
    public Color ColorPlata
    {
        get { return colorPlata; }
    }
    public Color ColorBronce
    {
        get { return colorBronce; }
    }

}
