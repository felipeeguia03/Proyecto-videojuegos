using UnityEngine;

public class FaroRotador : MonoBehaviour
{
    public float velocidadRotacion = 100f;

    void Update()
    {
        float direccion = Input.GetAxis("Horizontal"); // Usa flechas o A/D
        transform.Rotate(Vector3.forward * -direccion * velocidadRotacion * Time.deltaTime);
    }
}
