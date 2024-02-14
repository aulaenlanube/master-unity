using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniShooter : MonoBehaviour
{ 
    public static MiniShooter instance;

    //elementos de la UI
    [SerializeField] private TextMeshProUGUI textoFinPartida;
    [SerializeField] private TextMeshProUGUI textoPuntuacion;
    [SerializeField] private TextMeshProUGUI textoOleada;

    //personaje principal
    [SerializeField] private Transform personajePrincipal;

    //configuráción de juego
    [SerializeField] private float velocidadEnemigos = 5.0f;    
    [SerializeField] private float velocidadPersonajeCaminar = 5.0f;
    [SerializeField] private float velocidadPersonajeCorrer = 15.0f;
    [SerializeField] private float gravedad = -9.81f;
    [SerializeField] private float alturaSalto = 2.0f;
    [SerializeField] private float duracionCorrer = 3.0f;
    [SerializeField] private int ladoZonaRespawn = 40;
    [SerializeField] private float sensibilidadRaton = 10f;
    [SerializeField] private float limiteRotacionVertical = 45.0f;
    [SerializeField] private Color colorOro;
    [SerializeField] private Color colorPlata;
    [SerializeField] private Color colorBronce; 
    [SerializeField] private Vector3[] posicionesCamara;

    //variables de control
    private float velocidadPersonaje;
    private int puntuacionJugador;
    private int posicionActual;   
    private int oleadaActual;
    private int enemigosOleadaActual;
    private int enemigosRestantes;
    private List<EnemigoShooter> enemigosEliminados;
    private float tiempoCorrerRestante;

    private void Awake()
    {
        instance = this;
        Cursor.visible = false;                     // ocultamos el cursor       
        Cursor.lockState = CursorLockMode.Locked;   // bloqueamos el cursor en el centro de la pantalla    
    }

    void Start()
    {
        velocidadPersonaje = velocidadPersonajeCaminar;
        textoFinPartida.enabled = false;
        puntuacionJugador = 0;
        posicionActual = 0;
        oleadaActual = 1;
        enemigosOleadaActual = 3;
        tiempoCorrerRestante = duracionCorrer;
        enemigosRestantes = enemigosOleadaActual;
        enemigosEliminados = new List<EnemigoShooter>();
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

    public void AgregarEnemigoEliminado(EnemigoShooter enemigo)
    {
        enemigosEliminados.Add(enemigo);
        enemigosRestantes--;
        enemigo.gameObject.SetActive(false);

        //si se han eliminado todos los enemigos de la oleada
        if (enemigosRestantes == 0)
        {
            //se añade un enemigo más y se incrementa la oleada
            GameObject enemigoAdicional = Instantiate(enemigo.gameObject);
            enemigoAdicional.gameObject.SetActive(true);
            enemigoAdicional.GetComponent<EnemigoShooter>().CambiarPosicion();
            enemigosOleadaActual = ++oleadaActual + 2; //+2, de inicio tenemos 3 en la primera oleada
            enemigosRestantes = enemigosOleadaActual;

            //se vuelven a activar los enemigos eliminados y se les cambia la posición
            foreach (EnemigoShooter enemigoShooter in enemigosEliminados)
            {
                enemigoShooter.CambiarPosicion();
                enemigoShooter.gameObject.SetActive(true);
            }
            enemigosEliminados.Clear();    
            IncrementarVelocidadEnemigos();
        }        
    }

    // incrementa la velocidad de los enemigos en un 10%
    public void IncrementarVelocidadEnemigos()
    {
        velocidadEnemigos *= 1.1f;
    }

    public void Correr()
    {
        if (tiempoCorrerRestante > 0)
        {
            velocidadPersonaje = velocidadPersonajeCorrer;
            tiempoCorrerRestante = Mathf.Max(0, tiempoCorrerRestante - Time.deltaTime);            
        }
        else Caminar();
    }

    public void Caminar()
    {                
        if (tiempoCorrerRestante < duracionCorrer)
        {
            velocidadPersonaje = velocidadPersonajeCaminar;
            tiempoCorrerRestante  = Mathf.Min(duracionCorrer, tiempoCorrerRestante + Time.deltaTime);
        }        
    }

    public bool EstaCorriendo()
    {
        return velocidadPersonajeCorrer == velocidadPersonaje;
    }

    public float PorcentajeTiempoCorrer()
    {
        return tiempoCorrerRestante / duracionCorrer;        
    }

    public bool BarraCorrerLlena()
    {
        return tiempoCorrerRestante >= duracionCorrer;
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
        textoOleada.text = $"Oleada : {oleadaActual}\nEnemigosRestantes: {enemigosRestantes}";
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

    public int OleadaActual
    {
        get { return oleadaActual; }
        set { oleadaActual = value; }
    }

    public int EnemigosOleadaActual
    {
        get { return enemigosOleadaActual; }
        set { enemigosOleadaActual = value; }
    }

    public int EnemigosRestantes
    {
        get { return enemigosRestantes; }
        set { enemigosRestantes = value; }
    }
    public float DuracionCorrer
    {
        get { return duracionCorrer; }
    }
    public float TiempoCorrerRestante
    {
        get { return tiempoCorrerRestante; }
        set { tiempoCorrerRestante = value; }
    }
    public float AlturaSalto
    {
        get { return alturaSalto; }
    }
    public float Gravedad
    { 
        get { return gravedad; } 
    }
}
