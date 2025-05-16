using System.Collections.Generic;
using UnityEngine;

public class BoatPool : MonoBehaviour
{
    public BoatFactory factory;
    private Dictionary<BoatType, Queue<GameObject>> pool = new();

    public GameObject GetBoat(BoatType type)
    {
        if (!pool.ContainsKey(type))
            pool[type] = new Queue<GameObject>();

        if (pool[type].Count > 0)
        {
            var boat = pool[type].Dequeue();
            boat.SetActive(true);
            Debug.Log(" Reutilizando barco de tipo: " + type);
            return boat;
        }

        Debug.Log(" Creando nuevo barco de tipo: " + type);
        return factory.CreateBoat(type);
    }

    public void ReturnBoat(BoatType type, GameObject boat)
    {
        boat.SetActive(false);
        pool[type].Enqueue(boat);
        Debug.Log(" Barco devuelto al pool: " + type);
    }
}
