
using UnityEngine;

public class AutodestruirMarcaDisparo : MonoBehaviour
{
    [SerializeField] private float tiempoDeVida = 15f; 

    void Start()
    {
        Destroy(gameObject, tiempoDeVida); 
    }
}
