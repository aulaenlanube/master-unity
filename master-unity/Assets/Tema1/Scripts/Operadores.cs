using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Operadores : MonoBehaviour
{
    public Text texto;

    // Start is called before the first frame update
    void Start()
    {
        int precio1 = 5, precio2 = 10;
        int coste = precio1 * precio2 * 2;
        texto.text = $"coste: {coste}\n";            //100
        int media1 = precio1 + precio2 / 2;
        texto.text += $"media1: {media1}\n";         //10
        int media2 = (precio1 + precio2) / 2;
        texto.text += $"media2: {media2}\n";         //7
        float media3 = (precio1 + precio2) / 2;
        texto.text += $"media3: {media3}\n";         //7
        float media4 = (precio1 + precio2) / 2f;
        texto.text += $"media4: {media4}\n\n";       //7,5


        precio1 = 10;
        precio2 = 30;
        precio1 += precio2;                         //40
        texto.text += $"precio1: {precio1}\n";       
        precio2 -= precio1 * -2;                    //110
        texto.text += $"precio2: {precio2}\n";       
        bool barato = precio1 > precio2;            //False
        texto.text += $"barato: {barato}\n";
        bool iguales1 = 150 == (precio1 + precio2); //True 
        texto.text += $"iguales1: {iguales1}\n";
        bool iguales2 = 150 == precio1 + precio2;   //True
        texto.text += $"iguales2: {iguales2}\n";
        bool distintos = precio1 != precio2;        //True
        texto.text += $"distintos: {distintos}\n\n";

        int a = 1, b = 2, c = 5, d = 2;
        bool condicion1 = true, condicion2 = false, resultado;

        resultado = condicion1 && condicion2;     //False  
        texto.text += $"resultados: {resultado} |";
        resultado = condicion1 || condicion2;     //True  
        texto.text += $" {resultado} |";
        resultado = condicion1 && a < b;          //True  
        texto.text += $" {resultado} |";
        resultado = condicion2 && a < b;          //False  
        texto.text += $" {resultado} |";
        resultado = condicion1 && !condicion2;    //True 
        texto.text += $" {resultado} |";
        resultado = !(condicion1 && !condicion2); //False 
        texto.text += $" {resultado} |";
        resultado = !(condicion1 && condicion2);  //True 
        texto.text += $" {resultado} |";
        resultado = condicion1 ^ condicion2;      //True 
        texto.text += $" {resultado} |";
        resultado = condicion1 ^ a != b;          //False
        texto.text += $" {resultado} |";
        resultado = !condicion1 ^ !(a != b);      //False
        texto.text += $" {resultado} |";
        resultado = !(condicion1 ^ a != b);       //True
        texto.text += $" {resultado}\n\n";

        a = 10;
        b = 20;
        bool condicion = true;
        int expresion = a + b * c / d - (condicion ? a : b) + (b > a ? c : d) + (a + b) / (c + 1) - (d > 1 ? (c * d + a) : (b / d + a));
        texto.text += $"expresión: {expresion}\n";
    }


}
