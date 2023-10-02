using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemplosStrings2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int num1 = 1;
        int num2 = 2;
        float num3 = 0.5f;
        float num4 = 2f;
        float precio = 5.4567f;
        string frase1 = "Hola";
        string frase2 = "Mundo";
        Debug.Log(num1 + num2); //3
        Debug.Log(num1 + num2 + frase1 + frase2);    //3HolaMundo
        Debug.Log(frase1 + " " + frase2 + num1 + num2); //Hola Mundo12
        Debug.Log(frase1 + " " + frase2 + (num1 + num2)); //Hola Mundo3
        Debug.Log($"{frase1} {frase2}{num1+num2}"); //Hola Mundo3
        Debug.Log($"{frase1} {frase2}{num1}{num2}"); //Hola Mundo12
        Debug.Log(num3 + num4); //2.5
        Debug.Log($"{num3+num4}"); //2,5
        Debug.Log(frase1 + frase2 + "\n" + num3 + num4);    //HolaMundo 
                                                            //0,52
        Debug.Log($"El precio es {precio:F2}"); 	//El precio es 5.46

    }
    
}
