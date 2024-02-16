using UnityEngine;

public class ControladorMovimientoShooter : MonoBehaviour
{
    private float sensibilidadRaton;
    private float limiteRotacionVertical;
    private Vector2 velocidadRotacion;
    private float suavizado = 5f;
    private GameObject[] puntosTeletransporte;
    private CharacterController controlador;
    private Vector3 velocidadJugador;

    void Start()
    {
        sensibilidadRaton = MiniShooter.instance.SensibilidadRaton;
        limiteRotacionVertical = MiniShooter.instance.LimiteRotacionVertical;
        controlador = GetComponent<CharacterController>();
        puntosTeletransporte = GameObject.FindGameObjectsWithTag("Teletransporte");
    }

    void Update()
    {

        ControlMovimiento();
        ControlRotacion();
        ControlSalto();
        ControlDisparo();

    }

    void ControlMovimiento()
    {
        //controlamos si está corriendo
        if (Input.GetKey(KeyCode.LeftShift)) MiniShooter.instance.Correr();
        else MiniShooter.instance.Caminar();

        MoverPersonaje();
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

    private void ControlSalto()
    {
        //salto
        if (Input.GetKeyDown(KeyCode.Space) && controlador.isGrounded)
            velocidadJugador.y += Mathf.Sqrt(MiniShooter.instance.AlturaSalto * -2f * MiniShooter.instance.Gravedad);
    }


    private void ControlDisparo()
    {
        if (Input.GetMouseButton(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                hit.collider.gameObject.GetComponent<EnemigoShooter>()?.DestruirObjetivo();
                if (hit.collider.gameObject.CompareTag("Interactuable"))
                {
                    hit.rigidbody?.AddForceAtPosition(transform.forward * MiniShooter.instance.FuerzaDisparo, hit.point);
                }

            }
        }
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
            MiniShooter.instance.FinPartida();
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


