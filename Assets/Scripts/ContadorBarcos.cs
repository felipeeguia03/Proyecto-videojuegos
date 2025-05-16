using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContadorBarcos : MonoBehaviour
{
    
    int cantidadBarcosMuertos = 0;
    // Start is called before the first frame update
    void Start()
    {
        Emisor emi = GameObject.Find("ObjetoEmisor").GetComponent<Emisor>();
        Receptor rec = GameObject.Find("ObjetoReceptor").GetComponent<Receptor>();
        rec.Subscribirse(emi);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SumarBarcoMuerto()
    {
        cantidadBarcosMuertos++;
        Debug.Log("Barcos muertos: " + cantidadBarcosMuertos);
        GetComponent<Text>().text = "Barcos muertos: " + cantidadBarcosMuertos;
    }
}
