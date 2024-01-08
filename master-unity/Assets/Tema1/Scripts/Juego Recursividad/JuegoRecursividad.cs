using UnityEngine;
using UnityEngine.UI;

public class JuegoRecursividad : MonoBehaviour
{
    [Range(3, 10)]
    public int escala;

    [Range(0f, 0.1f)]
    public float separacion;

    [Range(5, 20)]
    public int cantidadClics;

    public Color[] coloresCubos;

    private GameObject[,] cuadriculaCubos;
    private int puntuacionAcual = 0;
    private float duracionActual = 0f;
    private int cantidadClicsActual = 0;
    private int comboActual = 0;
    private int lado;

    public delegate void actualizarPuntuacion(int puntos);
    public static event actualizarPuntuacion puntuacionActualizada;

    public delegate void actualizarTiempo(float tiempo);
    public static event actualizarTiempo tiempoActualizado;

    public delegate void actualizarClics(int clicsActuales, int clicsTotales);
    public static event actualizarClics clicsActualizados;

    public delegate void actualizarCombo(int combo);
    public static event actualizarCombo comboActualizado;

    void Start()
    {
        lado = escala * 2 + 1;
        cuadriculaCubos = new GameObject[lado, lado];
        RegenerarCuadricula();
        AjustarCamara();

        //publicamos los 4 eventos al inicio
        puntuacionActualizada?.Invoke(puntuacionAcual);
        tiempoActualizado?.Invoke(duracionActual);
        clicsActualizados?.Invoke(cantidadClicsActual, cantidadClics);
        comboActualizado?.Invoke(0);
    }

    void RegenerarCuadricula()
    {
        for (int x = 0; x < lado; x++)
        {
            for (int y = 0; y < lado; y++)
            {
                if (cuadriculaCubos[x, y] == null)
                {
                    GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubo.transform.position = new Vector3(x + x * separacion, y + y * separacion, 0);
                    cubo.name = $"cubo_{x}_{y}";
                    cuadriculaCubos[x, y] = cubo;
                }
            }
        }
        //cambiamos los colores de todos los cubos
        foreach (GameObject cubo in cuadriculaCubos)
        {
            cubo.GetComponent<Renderer>().material.color = coloresCubos[Random.Range(0, coloresCubos.Length)];
        }
    }

    private void AjustarCamara()
    {
        Vector3 posicionCamara = cuadriculaCubos[lado / 2, lado / 2].transform.position;        
        float distanciaCamara = lado + (lado / Camera.main.aspect);
        Camera.main.transform.position = posicionCamara - Vector3.forward * distanciaCamara;
    }

    void Update()
    {
        AjustarCamara();

        // partida finalizada
        if (cantidadClicsActual == cantidadClics) return;

        duracionActual += Time.deltaTime;
        tiempoActualizado?.Invoke(duracionActual); //  actualizamos duración con evento

        if (Input.GetMouseButtonDown(0))
        {            
            clicsActualizados?.Invoke(++cantidadClicsActual, cantidadClics); //  actualizamos clics con evento

            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                GameObject cuboSeleccionado = hit.collider.gameObject;
                DestruirCubosMismoColor(cuboSeleccionado);

                puntuacionActualizada?.Invoke(puntuacionAcual); // actualizamos puntuación con evento
                comboActualizado?.Invoke(comboActual);          // actualizamos combo con evento
                comboActual = 0;
                RegenerarCuadricula();
            }
        }
    }

    private void DestruirCubosMismoColor(GameObject cuboSeleccionado)
    {
        // buscamos la posición del cubo en la cuadrícula
        int posX = 0, posY = 0;
        bool encontrado = false;
        for (int x = 0; x < lado && !encontrado; x++)
        {
            for (int y = 0; y < lado && !encontrado; y++)
            {
                if (cuadriculaCubos[x, y] == cuboSeleccionado)
                {
                    posX = x;
                    posY = y;
                    encontrado = true;
                }
            }
        }
        DestruirCubosMismoColor(posX, posY, cuboSeleccionado.GetComponent<Renderer>().material.color);
    }

    private void DestruirCubosMismoColor(int x, int y, Color color)
    {
        //si los índices son válidos y el color coincide
        if (IndicesValidos(x,y) && cuadriculaCubos[x, y].GetComponent<Renderer>().material.color == color)
        {
            //incrementamos puntos
            puntuacionAcual++;
            comboActual++;
            Destroy(cuadriculaCubos[x, y]);
            cuadriculaCubos[x, y] = null;

            //destruimos recursivamente
            DestruirCubosMismoColor(x + 1, y, color); //derecha
            DestruirCubosMismoColor(x - 1, y, color); //izquierda
            DestruirCubosMismoColor(x, y - 1, color); //abajo
            DestruirCubosMismoColor(x, y + 1, color); //arriba
        }
    }

    private bool IndicesValidos(int x, int y)
    {
        return x >= 0
            && x < cuadriculaCubos.GetLength(0)
            && y >= 0
            && y < cuadriculaCubos.GetLength(1)
            && cuadriculaCubos[x, y] != null;
    }
}
