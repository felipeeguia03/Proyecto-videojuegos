using UnityEngine;
using UnityEngine.Events;

public class Emisor : MonoBehaviour
{
    // Lista de acciones (subscriptores) implementada como UnityEvent
    public UnityEvent myEvent = new UnityEvent();

    // Método que ejecuta todas las acciones suscritas
    public void Emitir()
    {
        myEvent.Invoke();
    }
}