using UnityEngine;

public class ControladorMovimientoShooter : MonoBehaviour
{    
    private float sensibilidadRaton;
    private float limiteRotacionVertical;
    private Rigidbody rb;
    private Vector2 velocidadRotacion;
    private float suavizado = 5f;
    private float fuerzaSalto = 100f;       

    void Start()
    {  
        sensibilidadRaton = MiniShooter.instance.SensibilidadRaton;
        limiteRotacionVertical = MiniShooter.instance.LimiteRotacionVertical;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // mover personaje con rigidbody          
        float velocidadX = Input.GetAxis("Horizontal") * MiniShooter.instance.VelocidadPersonaje;
        float velocidadZ = Input.GetAxis("Vertical") * MiniShooter.instance.VelocidadPersonaje;
        rb.velocity = transform.rotation * new Vector3(velocidadX, rb.velocity.y, velocidadZ);
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

        // mover personaje
        float movHorizontal = Input.GetAxis("Horizontal");        
        rb.velocity = new Vector2(movHorizontal * MiniShooter.instance.VelocidadPersonaje, rb.velocity.y);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    private void ControlDisparo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit)) hit.collider.gameObject.GetComponent<EnemigoShooter>()?.DestruirObjetivo();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Moneda"))
        {
            collider.GetComponent<MonedaShooter>().RecolectarMoneda();
        }
        if (collider.CompareTag("Enemigo") || collider.CompareTag("Pared"))
        {
            MiniShooter.instance.FinPartida();
        }
    }
}


