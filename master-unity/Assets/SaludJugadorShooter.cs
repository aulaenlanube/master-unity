using UnityEngine;
using UnityEngine.UI;

public class SaludJugadorShooter : MonoBehaviour
{
    [Range(10, 100)][SerializeField] private float saludMaxima = 10f;
    [Range(1, 10)][SerializeField] private float regeneracion = 1f;

    public static SaludJugadorShooter instance;

    private float saludActual;
    private Image[] imagenesSangre;


    void Start()
    {
        //singleton con la salud del jugador
        if (instance == null) instance = this;
        else Destroy(this);

        saludActual = saludMaxima;
        imagenesSangre = MiniShooter.instance.ImagenesSangre();
    }

    void Update()
    {
        if (!EstaVivo())
        {
            MiniShooter.instance.FinPartida();
        }
        else if (EstaVivo() && EstaHerido())
        {
            RegenerarVida();
        }
    }

    public void RecibirGlope(float dps)
    {
        saludActual -= dps * Time.deltaTime;
        saludActual = Mathf.Clamp(saludActual, 0, saludMaxima);
        ActualizarImagenSangre();
    }

    public void RegenerarVida()
    {
        saludActual += regeneracion * Time.deltaTime;
        saludActual = Mathf.Clamp(saludActual, 0, saludMaxima);
        ActualizarImagenSangre();
    }

    public bool EstaVivo()
    {
        return saludActual > 0;
    }

    public bool EstaHerido()
    {
        return saludActual < saludMaxima;
    }

    void ActualizarImagenSangre()
    {
        float alpha = 1 - (saludActual / saludMaxima);

        foreach (Image imagenSangre in imagenesSangre)
        {
            imagenSangre.color = new Color(.5f, .5f, .5f, alpha); // opacidad basada en la salud
        }
    }
}
