using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContadorVidas : MonoBehaviour
{
    public Image[] Corazones;
    public Sprite CorazonLleno;
    public Sprite CorazonVacio;
    public int vidas = 3;

    public void PerderVida()
    {
        if (vidas <= 0) return;

        vidas--;
        ActualizarCorazones();

        if (vidas == 0)
        {
            Restart();
        }
    }

    public void Restart(){
        SceneManager.LoadScene("menuScene");
    }

    void ActualizarCorazones()
    {
        for (int i = 0; i < Corazones.Length; i++)
        {
            if (i < vidas)
                Corazones[i].sprite = CorazonLleno;
            else
                Corazones[i].sprite = CorazonVacio;
        }
    }
}
