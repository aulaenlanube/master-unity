using UnityEngine;

public class EjemploSonidos : MonoBehaviour
{
    public AudioClip melodia1;
    public AudioClip melodia2;
    public AudioClip efecto1;
    public AudioClip efecto2;
    public AudioClip efecto3;

    private AudioSource fuenteDeMelodias;
    private AudioSource fuenteDeEfectos;

    void Start()
    {
        fuenteDeMelodias = gameObject.AddComponent<AudioSource>();
        fuenteDeEfectos = gameObject.AddComponent<AudioSource>();
        fuenteDeMelodias.loop = true;
    }

    void Update()
    {
        // control de melodías
        if (Input.GetKeyDown(KeyCode.A))
        {
            fuenteDeMelodias.clip = melodia1;
            fuenteDeMelodias.Play();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            fuenteDeMelodias.clip = melodia2;
            fuenteDeMelodias.Play();
        }
        
        // control de efectos de sonido
        if (Input.GetKeyDown(KeyCode.C)) fuenteDeEfectos.PlayOneShot(efecto1);        
        if (Input.GetKeyDown(KeyCode.V)) fuenteDeEfectos.PlayOneShot(efecto2);        
        if (Input.GetKeyDown(KeyCode.B)) fuenteDeEfectos.PlayOneShot(efecto3);   
    }
}

