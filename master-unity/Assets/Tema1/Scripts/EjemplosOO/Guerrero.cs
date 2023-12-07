using UnityEngine;

public class Guerrero : Personaje
{
    public Guerrero(string nombre, int vida) : base(nombre, vida) { }

    public override void RealizarAccionEspecial()
    {
        Debug.Log($"{Nombre} realiza un poderoso ataque");
    }
    public override void RecibirGolpe(int n) // sobrescritura
    {
        this.Vida -= Mathf.RoundToInt(n * 0.9f); // usa el setter con validación
        Debug.Log($"{Nombre} ha recibido {n * 0.9f} puntos de daño");
    }
}

