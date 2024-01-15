using TMPro;
using UnityEngine;

public class MiniShooter : MonoBehaviour
{
    //elementos de la UI
    [SerializeField] private TextMeshProUGUI textoFinPartida;


    //personaje principal
    [SerializeField] private GameObject personajePrincipal;
    

    public static MiniShooter instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void FinPartida()
    {
        textoFinPartida.enabled = true;
        Time.timeScale = 0;
    }

    public GameObject PersonajePrincipal()
    {
        return personajePrincipal;
    }   
}
