using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CapacidadZoom
{
    SinZoom = 60,
    X2 = 30,
    X3 = 20,
    X6 = 10,
    Max = 5
}

public enum VelocidadZoom
{
    Lenta = 5,
    Normal = 10,
    Rapida = 20,
    UltraRapida = 30
}


public class MiniShooter : MonoBehaviour
{
    public static MiniShooter instance;

    [Header("Personaje Principal")]
    [SerializeField] private Transform personajePrincipal;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textoFinPartida;
    [SerializeField] private TextMeshProUGUI textoPuntuacion;
    [SerializeField] private TextMeshProUGUI textoOleada;
    [SerializeField] private TextMeshProUGUI textoMunicion;
    [SerializeField] private Image municionUI;
    [SerializeField] private Image emblemaUI;
    [SerializeField] private Sprite[] emblemas;

    [Header("Configuración del movimiento")]
    [SerializeField] private float velocidadEnemigos = 5.0f;
    [SerializeField] private float velocidadPersonajeCaminar = 5.0f;
    [SerializeField] private float velocidadPersonajeCorrer = 15.0f;
    [SerializeField] private float gravedad = -9.81f;
    [SerializeField] private float alturaSalto = 2.0f;
    [SerializeField] private float duracionCorrer = 3.0f;

    [Header("Configuración de fuerzas")]
    [SerializeField] private float fuerzaEmpuje = 10f;
    [SerializeField] private float fuerzaDisparo = 10f;

    [Header("Configuración del disparo")]
    [SerializeField] private int municion = 100;
    [Range(0.05f, 1f)][SerializeField] private float velocidadDisparo = 1f;
    [Range(0.2f, 2f)][SerializeField] private float tiempoRecarga = 1;
    [SerializeField] private float fuerzaRetroceso = 5f;
    [SerializeField] private float velocidadRetorno = 25f;

    [Header("Configuración mirilla")]
    [SerializeField] private Image mirilla;
    [SerializeField] private Image mirillaZoom;
    [SerializeField] private VelocidadZoom velocidadZoom;
    [SerializeField] private CapacidadZoom capacidadZoom;

    [Header("Configuraciones cámara")]
    [SerializeField] private Vector3[] posicionesCamaraDePie;
    [SerializeField] private Vector3[] posicionesCamaraAgachado;

    [Header("Configuraciones adicionales")]
    [SerializeField] private RuntimeAnimatorController animatorEnemigoTipo1;
    [SerializeField] private GameObject prefabEnemigoTipo1;
    [SerializeField] private int ladoZonaRespawn = 40;
    [SerializeField] private float sensibilidadRaton = 10f;
    [SerializeField] private float limiteRotacionVertical = 45.0f;
    [SerializeField] private Color colorOro;
    [SerializeField] private Color colorPlata;
    [SerializeField] private Color colorBronce;


    //variables de control
    private List<EnemigoShooter> enemigosEliminados = new List<EnemigoShooter>();
    private int puntuacionJugador = 0;
    private int posicionActualCamara = 0;
    private int oleadaActual = 1;
    private int enemigosOleadaActual = 3;
    private float tiempoUltimoDisparo = 0;
    private float cantidadRetroceso = 0f;
    private bool barraCorrerVacia = false;
    private bool agachado = false;
    private bool recargando = false;

    //variables de control sin inicializar
    private Vector3[] posicionesCamara;
    private float velocidadPersonaje;
    private int enemigosRestantes;
    private float tiempoCorrerRestante;


    private void Awake()
    {
        instance = this;
        Cursor.visible = false;                     // ocultamos el cursor       
        Cursor.lockState = CursorLockMode.Locked;   // bloqueamos el cursor en el centro de la pantalla    
    }

    void Start()
    {
        posicionesCamara = posicionesCamaraDePie;
        velocidadPersonaje = velocidadPersonajeCaminar;
        textoFinPartida.enabled = false;
        tiempoCorrerRestante = duracionCorrer;
        enemigosRestantes = enemigosOleadaActual;
        textoMunicion.text = municion.ToString();


    }

    void Update()
    {
        // actualizamos el tiempo de disparo si no se está recargando
        if (!recargando) tiempoUltimoDisparo += Time.deltaTime;

        //cambio de cámara
        if (posicionesCamara.Length > 0 && Input.GetKeyDown(KeyCode.C))
        {
            if (posicionActualCamara == posicionesCamara.Length - 1) posicionActualCamara = 0;
            else posicionActualCamara++;
            EstablecerCamara();
        }
    }

    private void LateUpdate()
    {
        if (cantidadRetroceso > 0)
        {
            // aplicar el retroceso hacia arriba
            Camera.main.transform.Rotate(-cantidadRetroceso, 0f, 0f); // aplica el efecto de retroceso
            cantidadRetroceso -= velocidadRetorno * Time.deltaTime; // reduce gradualmente el efecto de retroceso
        }
    }

    public void FinPartida()
    {
        textoFinPartida.enabled = true;
        Time.timeScale = 0;
    }

    public void AlternarCamaras()
    {
        if (agachado) posicionesCamara = posicionesCamaraAgachado;
        else posicionesCamara = posicionesCamaraDePie;

        if (posicionesCamara.Length <= posicionActualCamara) posicionActualCamara = 0;
        EstablecerCamara();
    }

    public void ReniciarCamara()
    {
        posicionActualCamara = 0;
        EstablecerCamara();
    }

    void EstablecerCamara()
    {
        //alternamos la culling mask de la cámara para renderizar o no el personaje
        if (posicionActualCamara == 0) EliminarCapaDeCullingMask(Camera.main, "JugadorPrimeraPersona");
        if (posicionActualCamara == 1) AgregarCapaACullingMask(Camera.main, "JugadorPrimeraPersona");

        //establecemos la posición de la cámara
        Camera.main.transform.localPosition = posicionesCamara[posicionActualCamara];
        ActualizarMirilla();
    }

    public void ActualizarMirilla()
    {
        if (EstaEnPrimeraPersona())
        {
            mirilla.enabled = true;
        }
        else
        {
            mirilla.enabled = false;
            mirillaZoom.enabled = false;
        }
    }


    // agrega una capa a la Culling Mask de la cámara
    public void AgregarCapaACullingMask(Camera camara, string nombreCapa)
    {
        int capa = LayerMask.NameToLayer(nombreCapa);
        camara.cullingMask |= 1 << capa;
    }

    // elimina una capa de la Culling Mask de la cámara
    public void EliminarCapaDeCullingMask(Camera camara, string nombreCapa)
    {
        int capa = LayerMask.NameToLayer(nombreCapa);
        camara.cullingMask &= ~(1 << capa);
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
            GameObject enemigoAdicional = Instantiate(prefabEnemigoTipo1);
            enemigosOleadaActual = ++oleadaActual + 2; //+2, de inicio tenemos 3 en la primera oleada
            enemigosRestantes = enemigosOleadaActual;

            //se vuelven a activar los enemigos eliminados y se les cambia la posición
            foreach (EnemigoShooter enemigoShooter in enemigosEliminados)
            {
                enemigoShooter.ReiniciarEnemigo();
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
        if (tiempoCorrerRestante > 0 && !barraCorrerVacia && !agachado)
        {
            velocidadPersonaje = velocidadPersonajeCorrer;
            tiempoCorrerRestante = Mathf.Max(0, tiempoCorrerRestante - Time.deltaTime);
        }
        else
        {
            barraCorrerVacia = true;
            Caminar();
        }
    }

    public void Caminar()
    {
        if (tiempoCorrerRestante < duracionCorrer)
        {
            velocidadPersonaje = velocidadPersonajeCaminar;
            tiempoCorrerRestante = Mathf.Min(duracionCorrer, tiempoCorrerRestante + Time.deltaTime);
        }
        if (tiempoCorrerRestante == duracionCorrer) barraCorrerVacia = false;
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

    public void Disparar()
    {
        if (tiempoUltimoDisparo >= velocidadDisparo && municion > 0)
        {
            tiempoUltimoDisparo = 0;
            municion--;
            textoMunicion.text = municion.ToString();

            //actualizamos la barra de munición
            int municionRestanteCargador = municion % 10;
            municionUI.fillAmount = municionRestanteCargador * .1f;
            if (municionRestanteCargador == 0 && municion > 0) Recargar();

            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                hit.collider.gameObject.GetComponent<EnemigoShooter>()?.Impacto(1); //debe ir el daño del arma

                if (hit.collider.gameObject.CompareTag("Interactuable"))
                {
                    hit.rigidbody?.AddForceAtPosition(transform.forward * MiniShooter.instance.FuerzaDisparo, hit.point);
                }
            }
            AplicarRetroceso();
        }
    }

    void AplicarRetroceso()
    {
        cantidadRetroceso += fuerzaRetroceso;
    }

    void Recargar()
    {
        recargando = true;
        Invoke("CompletarRecarga", tiempoRecarga);
    }

    void CompletarRecarga()
    {
        recargando = false;
        tiempoUltimoDisparo = velocidadDisparo;
        municionUI.fillAmount = 1;
    }

    public Vector3 ObtenerPosicionCamaraActual()
    {
        return posicionesCamara[posicionActualCamara];
    }

    public bool EstaEnPrimeraPersona()
    {
        return posicionActualCamara == 0;
    }


    //---------------------------------------------
    //------------------ EVENTOS ------------------
    //---------------------------------------------
    private void OnEnable()
    {
        // se suscribe a los eventos de los enemigos
        EnemigoShooter.enemigoImpactado += ActualizarPuntuacion;
        MonedaShooter.monedaRecogida += IncrementarMunicion;
    }
    private void OnDisable()
    {
        // se desuscribe a los eventos de los enemigos
        EnemigoShooter.enemigoImpactado -= ActualizarPuntuacion;
        MonedaShooter.monedaRecogida -= IncrementarMunicion;
    }

    public void ActualizarPuntuacion(int puntos)
    {
        puntuacionJugador += puntos;
        textoPuntuacion.text = puntuacionJugador.ToString();
        textoOleada.text = $"{enemigosRestantes}";

        //actualizamos el emblema de la oleada
        int indiceEmblema = Mathf.Min(oleadaActual - 1, emblemas.Length - 1);
        emblemaUI.sprite = emblemas[indiceEmblema];
    }

    public void IncrementarMunicion(int cantidad)
    {
        municion += cantidad;
        textoMunicion.text = municion.ToString();
    }

    //---------------------------------------------
    //---------- GETTERS Y SETTERS ----------------
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

    public float AlturaSalto
    {
        get { return alturaSalto; }
    }

    public float Gravedad
    {
        get { return gravedad; }
    }

    public float FuerzaEmpuje
    {
        get { return fuerzaEmpuje; }
    }

    public float FuerzaDisparo
    {
        get { return fuerzaDisparo; }
    }

    public RuntimeAnimatorController AnimatorEnemigoTipo1
    {
        get { return animatorEnemigoTipo1; }
    }

    public bool Agachado
    {
        get { return agachado; }
        set { agachado = value; }
    }

    public Image Mirilla
    {
        get => mirilla;
    }

    public Image MirillaZoom
    {
        get => mirillaZoom;
    }
    public int VelocidadZoom
    {
        get => (int)velocidadZoom;
    }

    public int CapacidadZoom
    {
        get => (int)capacidadZoom;
    }
}
