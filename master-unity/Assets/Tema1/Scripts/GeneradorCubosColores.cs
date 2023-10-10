using UnityEngine;

public class GeneradorCubosColores : MonoBehaviour
{
    void Start()
    {
        int x = 0;
        while (x < 10)
        {
            GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubo.transform.position = new Vector3(x * 1.2f, 0, 0);
            cubo.GetComponent<Renderer>().material.color = (x % 2 == 0) ? Color.red : Color.blue;
            x++;
        }
    }
}
