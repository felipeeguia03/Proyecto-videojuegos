using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barco : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Morir()
    {
        Emisor emisorMuerte = GameObject.Find("ObjetoEmisor").GetComponent<Emisor>();
        emisorMuerte.Emitir();
    }
}
