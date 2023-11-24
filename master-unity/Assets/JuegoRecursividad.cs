using UnityEngine;
using UnityEngine.UI;

public class JuegoRecursividad : MonoBehaviour
{
    [Range(5, 10)]
    public int dimension;
    [Range(0f, 0.1f)]
    public float separacion;

    public Text textoPuntuacion;
    private GameObject[,] cuadriculaCubos;

    private int puntuacion = 0;

    void Start()
    {
        dimension = dimension * 2 + 1;
        cuadriculaCubos = new GameObject[dimension, dimension];
        GenerarCuadricula();
        Camera.main.transform.position = cuadriculaCubos[dimension / 2, dimension / 2].transform.position - Vector3.forward * dimension;
    }

    void GenerarCuadricula()
    {
        for (int x = 0; x < dimension; x++)
        {
            for (int y = 0; y < dimension; y++)
            {
                Vector3 posicion = new Vector3(x + x * separacion, y + y * separacion, 0);
                GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubo.transform.position = posicion;
                cubo.name = $"Cubo_{x}_{y}";
                
                cuadriculaCubos[x, y] = cubo;
                CambiarColorCubo(cubo);
            }
        }
    }

    void DestruirCuadricula()
    {
        for (int x = 0; x < dimension; x++)
        {
            for (int y = 0; y < dimension; y++)
            {
                if(cuadriculaCubos[x,y] != null) Destroy(cuadriculaCubos[x, y]);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                GameObject cuboSeleccionado = hit.collider.gameObject;                
                ContarCubosMismoColor(cuboSeleccionado);
                CambiarColoresAleatoriamente();


                textoPuntuacion.text = $"Puntos: {puntuacion}";
                DestruirCuadricula();
                GenerarCuadricula();
            }
            
        }
        
    }

    void ContarCubosMismoColor(GameObject cuboSeleccionado)
    {
        // Encuentra la posici?n del cubo en la cuadr?cula
        int posX = 0, posY = 0;
        bool encontrado = false;
        for (int x = 0; x < dimension && !encontrado; x++)
        {
            for (int y = 0; y < dimension && !encontrado; y++)
            {
                if (cuadriculaCubos[x, y] == cuboSeleccionado)
                {
                    posX = x;
                    posY = y;
                    encontrado = true;
                }
            }
        }
        ContarCubosMismoColor(posX, posY, cuboSeleccionado.GetComponent<Renderer>().material.color);
    }

    void ContarCubosMismoColor(int x, int y, Color color)
    {
        //comprobamos si los ?ndices son v?lidos
        if(IndicesValidos(x,y) && cuadriculaCubos[x, y] != null)
        {
            //si el color coincide
            if (cuadriculaCubos[x, y].GetComponent<Renderer>().material.color == color)
            {
                //incrementamos puntos
                puntuacion++;
                Destroy(cuadriculaCubos[x, y]);

                //buscamos recursivamente
                if (IndicesValidos(x+1, y)) ContarCubosMismoColor(x + 1, y, color); //arriba
                if (IndicesValidos(x-1, y)) ContarCubosMismoColor(x - 1, y, color); //abajo
                if (IndicesValidos(x, y-1)) ContarCubosMismoColor(x, y - 1, color); //izquierda
                if (IndicesValidos(x, y+1)) ContarCubosMismoColor(x, y + 1, color); //derecha
            }   
        }
    }

    bool IndicesValidos(int x, int y)
    {
        return x >= 0 && x <= cuadriculaCubos.Length - 1 && y >= 0 && y <= cuadriculaCubos.Length - 1;
    }


    void CambiarColoresAleatoriamente()
    {
        foreach (GameObject cubo in cuadriculaCubos)
        {
            CambiarColorCubo(cubo);
        }
    }

    void CambiarColorCubo(GameObject cubo)
    {
        Renderer rendererCubo = cubo.GetComponent<Renderer>();
        Color colorAleatorio = ObtenerColorAleatorio();
        rendererCubo.material.color = colorAleatorio;
    }

    Color ObtenerColorAleatorio()
    {
        Color[] colores = { Color.green, Color.red, Color.blue, Color.yellow, Color.cyan, Color.gray, Color.white }; 
        return colores[Random.Range(0, colores.Length)];
    }
}
