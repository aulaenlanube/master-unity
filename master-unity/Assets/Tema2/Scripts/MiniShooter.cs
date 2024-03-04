using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniShooter : MonoBehaviour
{
    public static MiniShooter instance;

    //elementos de la UI
    [SerializeField] private TextMeshProUGUI textoFinPartida;
    [SerializeField] private TextMeshProUGUI textoPuntuacion;
    [SerializeField] private TextMeshProUGUI textoOleada;
    [SerializeField] private TextMeshProUGUI textoMunicion;

    //personaje principal
    [SerializeField] private Transform personajePrincipal;

    //configuráción de juego
    [SerializeField] private float velocidadEnemigos = 5.0f;
    [SerializeField] private float velocidadPersonajeCaminar = 5.0f;
    [SerializeField] private float velocidadPersonajeCorrer = 15.0f;
    [SerializeField] private float gravedad = -9.81f;
    [SerializeField] private float alturaSalto = 2.0f;
    [SerializeField] private float fuerzaEmpuje = 10f;
    [SerializeField] private float fuerzaDisparo = 10f;
    [SerializeField] private int municion = 100;
    [Range(0.05f, 1f)][SerializeField] private float velocidadDisparo = 1f;
    [Range(0.2f, 2f)][SerializeField] private float tiempoRecarga = 1;
    [SerializeField] private float duracionCorrer = 3.0f;
    [SerializeField] private int ladoZonaRespawn = 40;
    [SerializeField] private float sensibilidadRaton = 10f;
    [SerializeField] private float limiteRotacionVertical = 45.0f;
    [SerializeField] private Color colorOro;
    [SerializeField] private Color colorPlata;
    [SerializeField] private Color colorBronce;

    [SerializeField] private Vector3[] posicionesCamaraDePie;
    [SerializeField] private Vector3[] posicionesCamaraAgachado;
    [SerializeField] private RuntimeAnimatorController animatorEnemigoTipo1;
    [SerializeField] private GameObject prefabEnemigoTipo1;
    [SerializeField] private Image municionUI;

    //variables de control
    private Vector3[] posicionesCamara;
    private float velocidadPersonaje;
    private int puntuacionJugador;
    private int posicionActual;
    private int oleadaActual;
    private int enemigosOleadaActual;
    private int enemigosRestantes;
    private List<EnemigoShooter> enemigosEliminados;
    private float tiempoCorrerRestante;
    private bool barraCorrerVacia;
    private bool agachado;
    private float tiempoUltimoDisparo;
    private bool recargando;
    

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
        puntuacionJugador = 0;
        posicionActual = 0;
        oleadaActual = 1;
        enemigosOleadaActual = 3;
        tiempoCorrerRestante = duracionCorrer;
        enemigosRestantes = enemigosOleadaActual;
        enemigosEliminados = new List<EnemigoShooter>();
        barraCorrerVacia = false;

        textoMunicion.text = municion.ToString();
        tiempoUltimoDisparo = 0;
        recargando = false;
    }

    void Update()
    {
        // actualizamos el tiempo de disparo si no se está recargando
        if (!recargando) tiempoUltimoDisparo += Time.deltaTime;

        //cambio de cámara
        if (posicionesCamara.Length > 0 && Input.GetKeyDown(KeyCode.C))
        {
            if (posicionActual == posicionesCamara.Length - 1) posicionActual = 0;
            else posicionActual++;
            Camera.main.transform.localPosition = posicionesCamara[posicionActual];
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

        if (posicionesCamara.Length <= posicionActual) posicionActual = 0;
        Camera.main.transform.localPosition = posicionesCamara[posicionActual];
    }

    public void ReniciarCamara()
    {
        posicionActual = 0;
        Camera.main.transform.localPosition = posicionesCamara[posicionActual];
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
            municionUI.fillAmount = municion % 10 * 0.1f;
            if (municionUI.fillAmount == 0) Recargar();

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
        }
    }

    void Recargar()
    {
        recargando = true;
        Invoke("CompletarRecarga", tiempoRecarga);
    }

    void CompletarRecarga()
    {
        recargando = false;
        municionUI.fillAmount = 1;
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
}
