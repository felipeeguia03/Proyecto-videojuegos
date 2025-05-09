using UnityEngine;

public class FaroRotador : MonoBehaviour
{
    public float velocidadMaxima = 250f;      // Velocidad máxima al rotar
    public float aceleracion = 2f;            // Qué tan rápido acelera (cuanto menor, más lenta)
    public float desaceleracion = 3f;         // Qué tan rápido frena
    private float intensidad = 0f;            // Va de 0 a 1 (cuánto estamos acelerando)
    private float direccionAnterior = 0f;

    void Update()
    {
        float direccion = Input.GetAxisRaw("Horizontal"); // -1, 0, 1

        if (direccion != 0)
        {
            // Si se mantiene la misma dirección, acelera
            if (direccion == direccionAnterior)
            {
                intensidad += Time.deltaTime / aceleracion;
            }
            else
            {
                // Si cambia de dirección, reinicia suavemente
                intensidad = 0.8f;
            }

            intensidad = Mathf.Clamp01(intensidad); // Siempre entre 0 y 1
            direccionAnterior = direccion;
        }
        else
        {
            // Desacelera cuando no hay input
            intensidad -= Time.deltaTime * desaceleracion;
            intensidad = Mathf.Clamp01(intensidad);
        }

        // Aceleración no lineal (suave, con curva)
        float curva = Mathf.SmoothStep(0f, 1f, intensidad); // o usá Mathf.Pow(intensidad, 2) para curva más fuerte

        // Aplica la rotación
        float velocidad = curva * velocidadMaxima;
        transform.Rotate(Vector3.forward * -direccionAnterior * velocidad * Time.deltaTime);
    }
}
