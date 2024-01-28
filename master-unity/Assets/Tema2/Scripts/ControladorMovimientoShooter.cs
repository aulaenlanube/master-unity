using UnityEngine;

public class ControladorMovimientoShooter : MonoBehaviour
{
    private float rotacionVertical;
    private float velocidadMovimiento;
    private float sensibilidadRaton;
    private float limiteRotacionVertical;

    void Start()
    {
        velocidadMovimiento = MiniShooter.instance.VelocidadPersonaje;
        sensibilidadRaton = MiniShooter.instance.SensibilidadRaton;
        limiteRotacionVertical = MiniShooter.instance.LimiteRotacionVertical;
        rotacionVertical = 0;
    }

    void Update()
    {
        // mover personaje
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * velocidadMovimiento * Time.deltaTime);

        // rotación horizontal
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensibilidadRaton, 0);

        // rotación vertical
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -limiteRotacionVertical, limiteRotacionVertical);
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionVertical, 0, 0);

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