using UnityEngine;

public class GeneradorCubos : MonoBehaviour
{
    public int ancho = 10;           // cantidad de cubos de lado
    public float separacion = 1.1f;  // distancia entre cubos

    void Start()
    {
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < ancho; j++)
            {
                GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubo.transform.position = new Vector3(i * separacion, 0, j * separacion);
            }
        }
    }
}
