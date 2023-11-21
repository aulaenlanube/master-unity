using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JuegoCubos : MonoBehaviour
{
    [SerializeField]
    private int puntuacionMaxima = 10;
    public GameObject jugador1;
    public GameObject jugador2;

    private bool juegoFinalizado = false;

    void Update()
    {
        if (jugador1.GetComponent<PuntuacionCubo>().puntos > puntuacionMaxima && !juegoFinalizado)
        {
            Debug.Log($"El jugador {jugador1.name} ha ganado");
            
            StartCoroutine(FinalizarJuego());
        }

        if (jugador2.GetComponent<PuntuacionCubo>().puntos > puntuacionMaxima && !juegoFinalizado)
        {
            Debug.Log($"El jugador {jugador1.name} ha ganado");
            StartCoroutine(FinalizarJuego());
        }        
    }

    IEnumerator FinalizarJuego()
    {
        juegoFinalizado = true;
        Time.timeScale = 0;
        yield return new WaitForSeconds(5f);
        Time.timeScale = 1; 
        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }

}
