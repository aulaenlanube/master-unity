using System;
using UnityEngine;

public class Zombi : IEnemigo, IGolpeable
{
    public const int MAX_RESISTENCIA = 50;
    public const int VIDA_POR_DEFECTO = 100;
    public static int cantidadZombis = 0;
    
    private int resistencia;
    private int vidaRestante;

    public int Resistencia
    {
        get { return resistencia; }
        set { resistencia = Math.Clamp(value, 0, MAX_RESISTENCIA); }  
    }

    public int VidaRestante
    {
        get { return vidaRestante; }
        set { vidaRestante = value; }
    }

    public Zombi(int resistencia) 
    { 
        Resistencia = resistencia;
        VidaRestante = VIDA_POR_DEFECTO;
        cantidadZombis++;
    }   

    public Zombi(int resistencia, int vidaRestante)
    {
        Resistencia = resistencia;
        VidaRestante = vidaRestante;
        cantidadZombis++;
    }
    
    public void Atacar() { Debug.Log("El zombi ataca"); }

    public void RecibirGolpe(int fuerza)
    {
        if (vidaRestante <= 0)  return;        
        int fuerzaReal = fuerza - resistencia;

        if(fuerzaReal <= 0)
        {
            Debug.Log("El zombi ha bloqueado el golpe");
            return;
        }

        Debug.Log($"El zombi ha sido golpeado con una fuerza de {fuerzaReal}");

        vidaRestante -= fuerzaReal;
        if (vidaRestante <= 0)
        {
            Debug.Log("El zombi ha muerto");
            cantidadZombis--;
        }
    }
}

