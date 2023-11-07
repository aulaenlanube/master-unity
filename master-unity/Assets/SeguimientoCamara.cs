using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    public Transform objetivo; 				// objeto que la cámara seguirá
    public float distancia = 10.0f; 			// distancia entre la cámara y el objeto
    public Vector3 offset = new Vector3(0, 0, 0);	// offset adicional

    void Update()
    {
        if (objetivo != null)
        {
            // calcular la nueva posición
            transform.position = objetivo.position - (objetivo.forward * distancia) + offset;
        }
    }
}
