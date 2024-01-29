using UnityEngine;

public class ControladorMovimientoShooter : MonoBehaviour
{
    private float velocidadMovimiento;
    private float sensibilidadRaton;
    private float limiteRotacionVertical;
    private Rigidbody rb;
    private Vector2 velocidadRotacion;
    private float suavizado = 5f;

    void Start()
    {
        velocidadMovimiento = MiniShooter.instance.VelocidadPersonaje;
        sensibilidadRaton = MiniShooter.instance.SensibilidadRaton;
        limiteRotacionVertical = MiniShooter.instance.LimiteRotacionVertical;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // mover personaje con rigidbody          
        float velocidadX = Input.GetAxis("Horizontal") * velocidadMovimiento;
        float velocidadZ = Input.GetAxis("Vertical") * velocidadMovimiento;
        rb.velocity = transform.rotation * new Vector3(velocidadX, rb.velocity.y, velocidadZ);
    }

    void Update()
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

        //disparo
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit)) hit.collider.gameObject.GetComponent<EnemigoShooter>()?.DestruirObjetivo();
        }
    }

    void OnTriggerEnter(Collider collider)
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


