using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContadorBarcos : MonoBehaviour
{

    int cantidadBarcosMuertos = 0;
    int cantidadBarcosIluminados = 0;

    public ContadorVidas contadorVidas;
    public TMP_Text textoContador;
    // Start is called before the first frame update
    void Start()
    {
        contadorVidas = GameObject.Find("ContenedorCorazones").GetComponent<ContadorVidas>();
        //Emisor[] emisores = GameObject.Find("Boat-Factory-Manager").GetComponents<Emisor>();
        //Receptor rec = GameObject.Find("ContadorBarcos").GetComponent<Receptor>();
        //rec.Subscribirse(emi);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = cantidadBarcosIluminados.ToString();
    }

    public void SumarBarcoMuerto()
    {
        cantidadBarcosMuertos++;
        Debug.Log("Barcos muertos: " + cantidadBarcosMuertos);
       // if (cantidadBarcosMuertos >= 3)
      //  {
       //     Restart();
      //  }

        contadorVidas.PerderVida();
    }

    public void SumarBarcoIluminado()
    {
        cantidadBarcosIluminados++;
        Debug.Log("Barcos iluminados: " + cantidadBarcosIluminados);
    }

    public void Restart()
    {
        SceneManager.LoadScene("FinalScene");
    }

}