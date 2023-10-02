using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EjemplosStrings : MonoBehaviour
{
    public Text textoUi;
    // Start is called before the first frame update
    void Start()
    {
        textoUi.text += string.Concat("Hola", " ", "Mundo") + "\n"; //Hola Mundo
        textoUi.text += "aulaenlanube.com".ToLower() + "\n"; //aulaenlanube.com
        textoUi.text += "aulaenlanube.com".ToUpper() + "\n"; //AULAENLANUBE.COM
        textoUi.text += "  aula en la nube   ".Trim() + "\n"; //aula en la nube
        textoUi.text += "aula en la nube".Substring(5, 5) + "\n"; ;  //en la
        textoUi.text += "aula en la nube".Replace("aula", "Unity") + "\n"; //Unity en la nube
        textoUi.text += "aulaenlanube.com".Length + "\n";   // 16
        textoUi.text += "aula en la nube".IndexOf("en") + "\n";  // 5
        textoUi.text += "aula en la nube".IndexOf("e") + "\n";  // 5
        textoUi.text += "aula en la nube".IndexOf("E") + "\n";  // -1
        textoUi.text += "aulaenlanube".StartsWith("aula") + "\n";  // True
        textoUi.text += "aulaenlanube".Contains("en") + "\n";  // True

        string[] partes = "aula en la nube".Split(' ');  // ["aula", "en", "la", "nube"]
        char[] caracteres = "aula".ToCharArray();  // ['a', 'u', 'l', 'a']

        textoUi.text += new string(caracteres) + "\n"; //aula
        textoUi.text += string.Join(" ",caracteres) + "\n";  //a u l a
        textoUi.text += string.Join(" ",partes) + "\n";  //aula en la nube
        textoUi.text += string.Compare(new string(caracteres), string.Join(" ", caracteres)) + "\n";  //mayor que 0
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
