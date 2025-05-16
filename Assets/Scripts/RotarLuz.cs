using UnityEngine;

public class RotarLuz : MonoBehaviour
{
    public float velocidadRotacion = 30f;

    void Update()
    {
        transform.Rotate(0f, 0f, velocidadRotacion * Time.deltaTime);
    }
}
