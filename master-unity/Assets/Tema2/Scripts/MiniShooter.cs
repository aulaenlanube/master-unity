using TMPro;
using UnityEngine;

public class MiniShooter : MonoBehaviour
{
    //elementos de la UI
    [SerializeField] private TextMeshProUGUI textoFinPartida;
    [SerializeField] private TextMeshProUGUI textoPuntuacion;

    //personaje principal
    [SerializeField] private Transform personajePrincipal;
    [SerializeField] private float velocidadEnemigos = 5.0f;
    [SerializeField] private float velocidadPersonaje = 20.0f;
    [SerializeField] private int ladoZonaRespawn = 40;
    [SerializeField] private float sensibilidadRaton = 10f;
    [SerializeField] private float limiteRotacionVertical = 45.0f; // límite de rotación vertical

    private int puntuacionJugador;

    public static MiniShooter instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        textoFinPartida.enabled = false;
        puntuacionJugador = 0;
    }

    public void FinPartida()
    {
        textoFinPartida.enabled = true;
        Time.timeScale = 0;
    }

    public Transform PersonajePrincipal()
    {
        return personajePrincipal.transform;
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
}
