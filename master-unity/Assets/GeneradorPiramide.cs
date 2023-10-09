using UnityEngine;

public class GeneradorPiramide : MonoBehaviour
{    
    private int nivelesPiramide = 10;           // niveles de la pir�mide
    private float separacionEntreCubos = 1.1f;  // distancia entre cubos
    private Vector3 posicionInicial;            // posici�n inicial para el primer cubo

    void Start()
    {
        // posici�n inicial para centrar la pir�mide
        float desplazamiento = (nivelesPiramide - 1) * separacionEntreCubos / 2;
        posicionInicial = new Vector3(-desplazamiento, 0, -desplazamiento);

        for (int nivelActual = nivelesPiramide; nivelActual > 0; nivelActual--)
        {
            int cubosEnNivelActual = 0;

            for (int y = 0; y < nivelActual; y++)
            {
                for (int x = 0; x < nivelActual; x++)
                {
                    // calculamos la posici�n para el nuevo cubo
                    Vector3 posicionNueva = posicionInicial + new Vector3(
                        x * separacionEntreCubos,
                        nivelesPiramide - nivelActual,
                        y * separacionEntreCubos
                    );

                    // creamos el cubo en la posici�n calculada
                    GameObject nuevoCubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    nuevoCubo.transform.position = posicionNueva;
                    cubosEnNivelActual++;
                }
            }

            // recalculamos la posici�n inicial para el nuevo nivel
            posicionInicial.x += separacionEntreCubos / 2;
            posicionInicial.y += 0;
            posicionInicial.z += separacionEntreCubos / 2;
        }
    }
}

