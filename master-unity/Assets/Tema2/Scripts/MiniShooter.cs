using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum CapacidadZoom
{
    SinZoom = 60,
    X2 = 30,
    X3 = 20,
    X6 = 10,
    X10 = 6,
    Max = 2
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
    [SerializeField] private Transform personajeTerceraPersona;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textoFinPartida;
    [SerializeField] private TextMeshProUGUI textoPuntuacion;
    [SerializeField] private TextMeshProUGUI textoOleada;
    [SerializeField] private TextMeshProUGUI textoMunicion;
    [SerializeField] private Image municionUI;
    [SerializeField] private Image emblemaUI;
    [SerializeField] private Sprite[] emblemas;
    [SerializeField] private Image[] imagenesSangre;

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
    [Range(1, 500)][SerializeField] private int capacidadCargador = 10;
    [SerializeField] private float fuerzaRetroceso = 5f;
    [SerializeField] private float velocidadRetorno = 25f;

    [Header("Configuración mirilla")]
    [SerializeField] private GameObject mirillaArma;
    [SerializeField] private Image mirillaPrimeraPersona;
    [SerializeField] private Image mirillaTerceraPersona;
    [SerializeField] private VelocidadZoom velocidadZoom;
    [SerializeField] private CapacidadZoom capacidadZoom;

    [Header("Configuraciones cámara")]
    [SerializeField] private Vector3[] posicionesCamaraDePie;
    [SerializeField] private Vector3[] posicionesCamaraAgachado;
    [SerializeField] private Camera camaraPrincipal;
    [SerializeField] private Camera camaraApuntando;

    [Header("Efectos de sonido")]
    [SerializeField] private AudioClip sonidoDisparo;
    [SerializeField] private AudioClip sonidoRecarga;
    [SerializeField] private AudioClip sonidoDolor;
    [SerializeField] private AudioClip sonidoMuerte;

    [Header("Configuraciones adicionales")]
    [SerializeField] private RuntimeAnimatorController animatorEnemigoTipo1;    
    [SerializeField] private GameObject armaTerceraPersona;
    [SerializeField] private GameObject prefabEnemigoTipo1;
    [SerializeField] private GameObject prefabMarcaDisparo;
    [SerializeField] private ParticleSystem efectoDisparoTerceraPersona;
    [SerializeField] private int ladoZonaRespawn = 40;
    [SerializeField] private float sensibilidadRaton = 10f;
    [SerializeField] private float limiteRotacionVertical = 45.0f;
    [SerializeField] private Color colorOro;
    [SerializeField] private Color colorPlata;
    [SerializeField] private Color colorBronce;
    [SerializeField] public GameObject suelo;


    [Header("Configuraciones de las mallas")]
    [SerializeField] private SkinnedMeshRenderer meshPersonaje;
    [SerializeField] private Mesh mallaBrazos;
    [SerializeField] private Mesh mallaCompleta;

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
    private bool finPartida = false;

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
        ActualizarBalasCargadorUI();
    }

    void Update()
    {
        // actualizamos el tiempo de disparo si no se está recargando
        if (!Recargando) tiempoUltimoDisparo += Time.deltaTime;

        //cambio de cámara
        if (posicionesCamara.Length > 0 && Input.GetKeyDown(KeyCode.C))
        {
            if (posicionActualCamara == posicionesCamara.Length - 1) posicionActualCamara = 0;
            else posicionActualCamara++;
            EstablecerCamara();
        }

        //si la partida ha terminado y se pulsa la tecla R, se reinicia la partida
        if (finPartida && Input.GetKeyDown(KeyCode.R)) ReiniciarPartida();
    }

    private void LateUpdate()
    {
        if (cantidadRetroceso > 0)
        {
            // aplicar el retroceso hacia arriba
            Camera.main.transform.Rotate(-cantidadRetroceso, 0f, 0f); // aplica el efecto de retroceso
            cantidadRetroceso -= velocidadRetorno * Time.deltaTime;   // reduce gradualmente el efecto de retroceso
        }
    }

    public void FinPartida()
    {
        if(!finPartida)
        {
            finPartida = true;

            DetenerEfectosDeSonido();
            SaludJugadorShooter.instance.GetComponent<AudioSource>().Stop();

            ReproducirSonidoMuerte();
            textoFinPartida.enabled = true;
            Time.timeScale = 0;
        }       
    }

    void ReiniciarPartida()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        //alternamos la culling mask de la cámara para renderizar las manos y el arma en primera persona
        if (EstaEnPrimeraPersona())
        {
            EliminarCapaDeCullingMask(Camera.main, "JugadorTerceraPersona");
            AgregarCapaACullingMask(Camera.main, "JugadorPrimeraPersona");
        }
        //alternamos la culling mask de la cámara para renderizar el personaje en tercera persona
        else
        {
            AgregarCapaACullingMask(Camera.main, "JugadorTerceraPersona");
            EliminarCapaDeCullingMask(Camera.main, "JugadorPrimeraPersona");
        }

        //establecemos la posición de la cámara
        Camera.main.transform.localPosition = posicionesCamara[posicionActualCamara];
        ActualizacionesFinalesCamara();        
    }

    public void ActualizacionesFinalesCamara()
    {
        if (EstaEnPrimeraPersona())
        {
            MirillaPrimeraPersona.enabled = true;
            MirillaTerceraPersona.enabled = false;

            meshPersonaje.sharedMesh = mallaBrazos;
        }
        else
        {
            MirillaTerceraPersona.enabled = true;
            MirillaPrimeraPersona.enabled = false;

            meshPersonaje.sharedMesh = mallaCompleta;
        }
    }

    public Vector3 PosCamaraActual()
    {
        return posicionesCamara[posicionActualCamara];

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

        ActualizarPuntuacion(enemigo.PuntosEnemigo);
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

    void ActualizarBalasCargadorUI()
    {
        int municionRestanteCargador = municion % capacidadCargador;
        if (municionRestanteCargador == 0 && municion > 0) municionRestanteCargador = capacidadCargador;
        if (municionRestanteCargador > 9) municionRestanteCargador = 10;
        municionUI.fillAmount = municionRestanteCargador * .1f;
    }

    public void Disparar()
    {
        if (tiempoUltimoDisparo >= velocidadDisparo && municion > 0)
        {
            tiempoUltimoDisparo = 0;
            municion--;
            textoMunicion.text = municion.ToString();

            //actualizamos las balas del cargador            
            ActualizarBalasCargadorUI();

            //obtenemos la posición del centro de la pantalla   
            Vector3 puntoPantalla = Input.mousePosition;

            //si estamos apuntando en primera persona, obtenemos la posición en pantalla a través de la mirilla
            if(Input.GetMouseButton(1) && EstaEnPrimeraPersona())
            {
                puntoPantalla = Camera.main.WorldToScreenPoint(mirillaArma.transform.position);
            }

            Ray rayo = Camera.main.ScreenPointToRay(puntoPantalla);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                hit.collider.gameObject.GetComponent<EnemigoShooter>()?.Impacto(1); //debe ir el daño del arma

                if (hit.collider.gameObject.CompareTag("Interactuable"))
                {
                    hit.rigidbody?.AddForceAtPosition(transform.forward * fuerzaDisparo, hit.point);
                }

                if (hit.collider.gameObject.CompareTag("Marcable"))
                {
                    MarcarDisparo(hit.point, hit.normal);
                }
            }

            personajeTerceraPersona.GetComponent<Animator>().SetBool("disparando", true);
            GetComponent<AudioSource>().PlayOneShot(sonidoDisparo);
            AplicarRetroceso();

            int municionRestanteCargador = municion % capacidadCargador;
            if (municionRestanteCargador == 0 && municion > 0) Recargar();
        }
    }

    void MarcarDisparo(Vector3 posicion, Vector3 normal)
    {
        // la rotación se ajusta para que la marca mire hacia afuera de la superficie
        GameObject marcaDisparo = Instantiate(prefabMarcaDisparo, posicion, Quaternion.LookRotation(normal));

        // ajustar la posición para que la marca no penetre la pared o flote sobre ella
        marcaDisparo.transform.position += marcaDisparo.transform.forward * 0.01f;
    }

    void AplicarRetroceso()
    {
        cantidadRetroceso += fuerzaRetroceso;
    }

    void Recargar()
    {
        personajeTerceraPersona.GetComponent<Animator>().SetBool("recargando", true);
        recargando = true;
    }


    public void CompletarRecarga()
    {
        personajeTerceraPersona.GetComponent<Animator>().SetBool("recargando", false);
        recargando = false;

        tiempoUltimoDisparo = velocidadDisparo;
        ActualizarBalasCargadorUI();
    }


    public Vector3 ObtenerPosicionCamaraActual()
    {
        return posicionesCamara[posicionActualCamara];
    }

    public bool EstaEnPrimeraPersona()
    {
        return posicionActualCamara == 0;
    }

    public void ReproducirSonidoRecarga()
    {
        GetComponent<AudioSource>().PlayOneShot(sonidoRecarga);
    }

    public void ReproducirSonidoDolor()
    {
        GetComponent<AudioSource>().PlayOneShot(sonidoDolor);
    }

    public void ReproducirSonidoMuerte()
    {
        GetComponent<AudioSource>().PlayOneShot(sonidoMuerte);
    }

    public void DetenerEfectosDeSonido()
    {
        GetComponent<AudioSource>().Stop();
    }

    //---------------------------------------------
    //------------------ EVENTOS ------------------
    //---------------------------------------------
    private void OnEnable()
    {
        MonedaShooter.monedaRecogida += IncrementarMunicion;
    }
    private void OnDisable()
    {
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
        ActualizarBalasCargadorUI();
    }

    public Image MirillaActual()
    {
        return EstaEnPrimeraPersona() ? MirillaPrimeraPersona : MirillaTerceraPersona;
    }

    public Vector2 ObtenerEscalaOriginalMirilla()
    {
        return MirillaActual().rectTransform.sizeDelta;
    }


    //---------------------------------------------
    //---------- GETTERS Y SETTERS ----------------
    //---------------------------------------------

    public Transform PersonajeTerceraPersona
    {
        get { return personajeTerceraPersona.transform; }
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

    public int VelocidadZoom
    {
        get => (int)velocidadZoom;
    }

    public int CapacidadZoom
    {
        get => (int)capacidadZoom;
    }

    public Image MirillaPrimeraPersona
    {
        get => mirillaPrimeraPersona;
        set => mirillaPrimeraPersona = value;
    }

    public Image MirillaTerceraPersona
    {
        get => mirillaTerceraPersona;
        set => mirillaTerceraPersona = value;
    }
    public bool Recargando
    {
        get => recargando;
        set => recargando = value;
    }
    public GameObject ArmaTerceraPersona
    {
        get => armaTerceraPersona;
        set => armaTerceraPersona = value;
    }

    public ParticleSystem EfectoDisparoTerceraPersona
    {
        get { return efectoDisparoTerceraPersona; }
    }

    public Image[] ImagenesSangre()
    {
        return imagenesSangre;
    }

    public Camera CamaraPrincipal
    {
        get { return camaraPrincipal; }
    }

    public Camera CamaraApuntando
    {
        get { return camaraApuntando; }
    }
}
