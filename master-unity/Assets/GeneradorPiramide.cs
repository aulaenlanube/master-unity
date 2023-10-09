using UnityEngine;

public class GeneradorPiramide : MonoBehaviour
{    
    private int nivelesPiramide = 10;           // niveles de la pirámide
    private float separacionEntreCubos = 1.1f;  // distancia entre cubos
    private Vector3 posicionInicial;            // posición inicial para el primer cubo

    void Start()
    {
        // posición inicial para centrar la pirámide
        float desplazamiento = (nivelesPiramide - 1) * separacionEntreCubos / 2;
        posicionInicial = new Vector3(-desplazamiento, 0, -desplazamiento);

        for (int nivelActual = nivelesPiramide; nivelActual > 0; nivelActual--)
        {
            int cubosEnNivelActual = 0;

            for (int y = 0; y < nivelActual; y++)
            {
                for (int x = 0; x < nivelActual; x++)
                {
                    // calculamos la posición para el nuevo cubo
                    Vector3 posicionNueva = posicionInicial + new Vector3(
                        x * separacionEntreCubos,
                        nivelesPiramide - nivelActual,
                        y * separacionEntreCubos
                    );

                    // creamos el cubo en la posición calculada
                    GameObject nuevoCubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    nuevoCubo.transform.position = posicionNueva;
                    cubosEnNivelActual++;
                }
            }

            // recalculamos la posición inicial para el nuevo nivel
            posicionInicial.x += separacionEntreCubos / 2;
            posicionInicial.y += 0;
            posicionInicial.z += separacionEntreCubos / 2;
        }
    }
}

