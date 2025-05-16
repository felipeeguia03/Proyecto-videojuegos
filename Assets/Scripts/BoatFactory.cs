using UnityEngine;

public class BoatFactory : MonoBehaviour
{
    public GameObject lanchaPrefab;
    public GameObject barcoComunPrefab;
    public GameObject cruceroPrefab;

    public GameObject CreateBoat(BoatType type)
    {
        switch (type)
        {
            case BoatType.Lancha: return Instantiate(lanchaPrefab);
            case BoatType.BarcoComun: return Instantiate(barcoComunPrefab);
            case BoatType.Crucero: return Instantiate(cruceroPrefab);
            default: return null;
        }
    }
}
