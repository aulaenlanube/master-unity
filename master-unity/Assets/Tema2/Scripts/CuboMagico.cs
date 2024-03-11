using UnityEngine;


public class CuboMagico : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = Vector3.zero;
    }
}
