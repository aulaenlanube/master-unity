using UnityEngine;

public class ControladorMovimientoShooter : MonoBehaviour
{
    private float sensibilidadRaton;
    private float limiteRotacionVertical;
    private Vector2 velocidadRotacion;
    private float suavizado = 1f;
    private GameObject[] puntosTeletransporte;
    private CharacterController controlador;
    private Vector3 velocidadJugador;
    private Animator animator;

    // variables para el cabeceo
    private float amplitudCabeceo = 0.05f;
    private float frecuenciaCabeceo = 10f;
    private float tiempoCabeceo = 0f;

    void Start()
    {
        sensibilidadRaton = MiniShooter.instance.SensibilidadRaton;
        limiteRotacionVertical = MiniShooter.instance.LimiteRotacionVertical;
        controlador = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        puntosTeletransporte = GameObject.FindGameObjectsWithTag("Teletransporte");
    }

    void Update()
    {
        ControlMovimiento();
        SimularCabeceo();
        ControlRotacion();        
        ControlDisparo();        
    }

    private void SimularCabeceo()
    {
        // simulamos el cabeceo cuando no está quieto y está en primera persona
        if (!animator.GetBool("quieto") && MiniShooter.instance.EstaEnPrimeraPersona())
        {
            // cálculo del tiempo para el efecto de cabeceo basado en el movimiento
            amplitudCabeceo = MiniShooter.instance.VelocidadPersonaje / 50f;
            frecuenciaCabeceo = MiniShooter.instance.VelocidadPersonaje;
            tiempoCabeceo += Time.deltaTime * frecuenciaCabeceo;
            float alturaCabeceo = Mathf.Sin(tiempoCabeceo) * amplitudCabeceo;

            // ajustar la posición de la cámara en el eje Y para simular el cabeceo
            Camera.main.transform.localPosition = new Vector3(0, alturaCabeceo, 0) + MiniShooter.instance.ObtenerPosicionCamaraActual(); 
        }       
    }

    void ControlMovimiento()
    {
        //controlamos si está corriendo
        if (Input.GetKey(KeyCode.LeftShift)) MiniShooter.instance.Correr();
        else MiniShooter.instance.Caminar();

        //controlamos si está agachado
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!animator.GetBool("agachado"))
            {
                animator.SetBool("agachado", true);
                MiniShooter.instance.Agachado = true;
                controlador.height = 1f;
                controlador.center = new Vector3(0, 0.6f, 0);
                MiniShooter.instance.AlternarCamaras();
            }
            else
            {
                if (PuedeLevantarse())
                {
                    animator.SetBool("agachado", false);
                    MiniShooter.instance.Agachado = false;
                    controlador.height = 1.4f;
                    controlador.center = new Vector3(0, 0.8f, 0);
                    MiniShooter.instance.AlternarCamaras();
                }
            }
        }
        MoverPersonaje();
    }

    bool PuedeLevantarse()
    {
        Vector3 limiteSuperiorPosicion = transform.position;
        limiteSuperiorPosicion.y += controlador.height;

        if (Physics.Raycast(limiteSuperiorPosicion, Vector3.up, 1f)) return false;
        return true;
    }


    void MoverPersonaje()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");
        Vector3 movimiento = transform.right * movimientoX + transform.forward * movimientoZ;
        controlador.Move(movimiento * MiniShooter.instance.VelocidadPersonaje * Time.deltaTime);

        //gravedad
        if (controlador.isGrounded && velocidadJugador.y < 0) velocidadJugador.y = 0f;
        velocidadJugador.y += MiniShooter.instance.Gravedad * Time.deltaTime;
        controlador.Move(velocidadJugador * Time.deltaTime);

        //--ANIMACIONES--

        //configurar animaciones
        animator.SetFloat("movimientoX", movimientoX * MiniShooter.instance.VelocidadPersonaje);
        animator.SetFloat("movimientoZ", movimientoZ * MiniShooter.instance.VelocidadPersonaje);

        //quieto o movimiento
        if (movimientoX == 0 && movimientoZ == 0) animator.SetBool("quieto", true);
        else animator.SetBool("quieto", false);

        //detener salto
        if (animator.GetBool("saltando") && controlador.isGrounded) animator.SetBool("saltando", false);

        //salto
        if (Input.GetKeyDown(KeyCode.Space) && controlador.isGrounded)
        {
            if (animator.GetBool("quieto")) Invoke("Saltar", .01f);
            else Saltar();

            animator.SetBool("saltando", true);
        }
    }

    void Saltar()
    {
        velocidadJugador.y = Mathf.Sqrt(MiniShooter.instance.AlturaSalto * -2f * MiniShooter.instance.Gravedad);
        animator.SetBool("saltando", true);
    }


    private void ControlRotacion()
    {
        // obtener movimiento del ratón con sensibilidad
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * sensibilidadRaton;

        // suavizar la rotación
        velocidadRotacion.x = Mathf.Lerp(velocidadRotacion.x, velocidadRotacion.x + mouseDelta.x, suavizado * Time.deltaTime);
        velocidadRotacion.y = Mathf.Lerp(velocidadRotacion.y, velocidadRotacion.y + mouseDelta.y, suavizado * Time.deltaTime);

        // limitamos rotación vertical
        velocidadRotacion.y = Mathf.Clamp(velocidadRotacion.y, -limiteRotacionVertical, limiteRotacionVertical);

        //rotación personaje y cámara
        Camera.main.transform.localRotation = Quaternion.AngleAxis(-velocidadRotacion.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(velocidadRotacion.x, Vector3.up);
    }




    private void ControlDisparo()
    {
        if (Input.GetMouseButton(0) && !MiniShooter.instance.Recargando)
        {
            MiniShooter.instance.Disparar();
        }
        else animator.SetBool("disparando", false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Moneda"))
        {
            collider.GetComponent<MonedaShooter>().RecolectarMoneda();
        }
        if (collider.gameObject.CompareTag("Teletransporte"))
        {
            controlador.enabled = false;
            int indice;
            Vector3 pos = new Vector3();
            do
            {
                indice = Random.Range(0, puntosTeletransporte.Length);
                pos = puntosTeletransporte[indice].transform.position;
            }
            while (puntosTeletransporte[indice] == collider.gameObject);
            transform.position = new Vector3(pos.x + 1, pos.y - 1, pos.z + 1);
            controlador.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            //MiniShooter.instance.FinPartida();
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // referencia al Rigidbody del objeto con el que se colisiona
        Rigidbody rb = hit.collider.attachedRigidbody;
        // si no hay Rigidbody o el Rigidbody es kinemático, no hacemos nada
        if (rb == null || rb.isKinematic) return;
        // si colisionamos con un objeto que podemos interaccionar
        if (hit.gameObject.CompareTag("Interactuable"))
        {
            // calculamos la dirección y la fuerza del empuje
            Vector3 direccionDeFuerza = hit.gameObject.transform.position - transform.position;
            // normalizamos la dirección para tener una magnitud de 1
            direccionDeFuerza.y = 0; // esto asegura que la fuerza se aplique horizontalmente
            direccionDeFuerza.Normalize();
            // aplicamos la fuerza al Rigidbody
            rb.AddForce(direccionDeFuerza * MiniShooter.instance.FuerzaEmpuje);
        }
    }




}




