using System.Collections.Generic;
using UnityEngine;

public class EjemploInterfaces : MonoBehaviour
{
    private List<IEnemigo> enemigos;
    private List<IGolpeable> enemigosGolpeables;

    void Start()
    {
        Zombi z1 = new Zombi(40);
        Zombi z2 = new Zombi(70);
        Robot r1 = new Robot();
        enemigos = new List<IEnemigo> { z1, z2, r1 };
        enemigosGolpeables = new List<IGolpeable> { z1, z2 };

        AtaqueEnemigos();
        GolpearEnemigos(100);   
        Debug.Log($"Cantidad de zombis: {Zombi.cantidadZombis}");
        GolpearEnemigos(50);
        Debug.Log($"Cantidad de zombis: {Zombi.cantidadZombis}");
        GolpearEnemigos(90);
        Debug.Log($"Cantidad de zombis: {Zombi.cantidadZombis}");

    
        
    }

    public void AtaqueEnemigos()
    {
        foreach (var enemigo in enemigos) enemigo.Atacar();
    }

    public void GolpearEnemigos(int fuerza)
    {
        foreach (var enemigo in enemigosGolpeables) enemigo.RecibirGolpe(fuerza);
    }


}
