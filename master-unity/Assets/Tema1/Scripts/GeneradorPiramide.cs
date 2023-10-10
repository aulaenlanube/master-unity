using UnityEngine;

public class GeneradorPiramide : MonoBehaviour
{
    public int niveles = 10;        // niveles de la pirámide
    public float separacion = 1.1f;    // distancia entre cubos

    void Start()
    {
        Vector3 inicio = Vector3.zero;
        for (int y = 0; y < niveles; y++)
        {
            for (int x = 0; x < niveles - y; x++)
            {
                for (int z = 0; z < niveles - y; z++)
                {
                    GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubo.transform.position = inicio + new Vector3(x * separacion, y, z * separacion);
                }
            }
            // calculamos la posición inicial para el nuevo nivel
            inicio.x += separacion / 2;
            inicio.z += separacion / 2;
        }
    }
}



