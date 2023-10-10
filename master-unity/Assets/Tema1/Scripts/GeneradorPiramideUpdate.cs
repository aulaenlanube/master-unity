using UnityEngine;

public class GeneradorPiramideUpdate : MonoBehaviour
{
    private int nivelesPiramide = 10;                       // n�mero de niveles de la pir�mide
    private float separacionEntreCubos = 1.1f;              // distancia entre cubos
    private int nivelActual = 10;                           // nivel actual en el que estamos creando cubos
    private int cubosEnNivelActual = 0;                     // cubos que hemos creado en el nivel actual
    private Vector3 posicionInicial = new Vector3(0, 0, 0); // posici�n del primer cubo    
    private Color color = Color.black;

    void Start()
    {
        // calcular la posici�n inicial para centrar la pir�mide
        float desplazamiento = (nivelesPiramide - 1) * separacionEntreCubos / 2;
        posicionInicial = new Vector3(-desplazamiento, 0, -desplazamiento);
    }

    void FixedUpdate()
    {
        if (nivelActual > 0)
        {
            // calculamos la posici�n para el nuevo cubo
            Vector3 posicionNueva = posicionInicial + new Vector3(
                (cubosEnNivelActual % nivelActual) * separacionEntreCubos,
                nivelesPiramide - nivelActual,
                Mathf.Floor(cubosEnNivelActual / nivelActual) * separacionEntreCubos
            );

            // creamos el cubo en la posici�n calculada
            GameObject nuevoCubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            nuevoCubo.transform.position = posicionNueva;
            nuevoCubo.GetComponent<Renderer>().material.color = color;

            // actualizamos el n�mero de cubos creados y la posici�n para el siguiente
            cubosEnNivelActual++;

            // verificamos si hemos completado el nivel actual de la pir�mide
            if (cubosEnNivelActual >= nivelActual * nivelActual)
            {
                // pasamos al siguiente nivel
                nivelActual--;

                // reiniciamos la cantidad de cubos en el nivel actual
                cubosEnNivelActual = 0;

                // recalculamos la posici�n inicial para el nuevo nivel
                posicionInicial.x += separacionEntreCubos / 2;
                posicionInicial.y += 0;
                posicionInicial.z += separacionEntreCubos / 2;

                //cambiamos color para la siguiente fila
                color = Random.ColorHSV();
            }
        }
    }
}
