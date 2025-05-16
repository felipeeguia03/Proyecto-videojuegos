using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Receptor : MonoBehaviour
{
    // Acción configurable que se ejecuta cuando un Emisor emite
    public UnityEvent mi_accion = new UnityEvent();

    // Lista de Emisores a los que está suscrito este Receptor
    public List<Emisor> subscripciones = new List<Emisor>();

    // Método para suscribirse a un Emisor
    public void Subscribirse(Emisor emisor)
    {
        // Añade la acción del Receptor al UnityEvent del Emisor
        emisor.myEvent.AddListener(mi_accion.Invoke);
        // Registra el Emisor en la lista de suscripciones
        subscripciones.Add(emisor);
    }
}