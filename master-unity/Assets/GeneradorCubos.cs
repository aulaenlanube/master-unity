using UnityEngine;

public class GeneradorCubos : MonoBehaviour
{
    private float tiempoEntreCubos = 0.1f; // tiempo entre cada cubo en segundos
    private float tiempoUltimoCubo; // tiempo desde el �ltimo cubo creado
    private int numCubosPorLado = 10; // n�mero de cubos por lado del cuadrado
    private float separacionEntreCubos = 1.1f; // distancia entre cubos
    private int cubosCreados = 0; // n�mero total de cubos creados
    private int filaActual = 0; // fila actual en la que estamos creando cubos
    private int columnaActual = 0; // columna actual en la que estamos creando cubos

    void Update()
    {
        if (Time.time - tiempoUltimoCubo > tiempoEntreCubos && cubosCreados < numCubosPorLado * numCubosPorLado)
        {
            // Calcular la posici�n para el nuevo cubo
            Vector3 posicionNueva = new Vector3(filaActual * separacionEntreCubos, 0, columnaActual * separacionEntreCubos);

            // Crear el cubo en la posici�n calculada
            GameObject nuevoCubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            nuevoCubo.transform.position = posicionNueva;

            // Actualizar el tiempo del �ltimo cubo creado
            tiempoUltimoCubo = Time.time;

            // Actualizar el n�mero de cubos creados y la posici�n para el siguiente
            cubosCreados++;
            filaActual++;

            if (filaActual >= numCubosPorLado)
            {
                filaActual = 0;
                columnaActual++;
            }
        }
    }
}
