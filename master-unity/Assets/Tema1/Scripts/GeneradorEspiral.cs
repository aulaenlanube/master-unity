using UnityEngine;

public class GeneradorEspiral : MonoBehaviour
{    
    public float radio = 10f; // radio de la espiral    
    public int vueltas = 3; // cantidad de vueltas    
    public int puntosPorVuelta = 100; // cantidad de puntos por vuelta
    public float indiceSubida = 0.1f;
    
    void Start()
    {
        int i = 0;
        do
        {
            float angulo = Mathf.PI * 2 * i / puntosPorVuelta;
            float x = radio * Mathf.Cos(angulo);
            float z = radio * Mathf.Sin(angulo);
            float y = i * indiceSubida;

            GameObject nuevoCubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            nuevoCubo.transform.position = new Vector3(x, y, z);

            radio = (radio <= 5) ? 5 : radio - 0.05f;
            i++;

        } while (i < vueltas * puntosPorVuelta);
    }
}
