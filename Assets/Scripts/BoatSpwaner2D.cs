using UnityEngine;

public class BoatSpawner2D : MonoBehaviour
{
    public BoatPool pool;
    public Transform lighthouseCenter;
    public float spawnRadius = 12f;
    public float spawnInterval = 2f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBoat();
            timer = 0f;
        }
    }

void SpawnBoat()
{
    Vector2 dir = Random.insideUnitCircle.normalized;
    Vector3 spawnPos = lighthouseCenter.position + new Vector3(dir.x, dir.y, 0) * spawnRadius;

    BoatType type = (BoatType)Random.Range(0, 3);
    GameObject boat = pool.GetBoat(type);

    boat.transform.position = spawnPos;
    boat.transform.rotation = Quaternion.identity;

    BoatMovement2D mover = boat.GetComponent<BoatMovement2D>();
    mover.speed = GetSpeed(type);
    mover.Initialize(type, pool);
    mover.SetTarget(lighthouseCenter.position);


    if (type == BoatType.BarcoComun)
    {
        FindFirstObjectByType<SoundManager>().Play("BarcoComun");
    }

        if (type == BoatType.Lancha)
    {
        FindFirstObjectByType<SoundManager>().Play("Lancha");
    }

        if (type == BoatType.Crucero)
    {
        FindFirstObjectByType<SoundManager>().Play("Crucero");
    }
}


    float GetSpeed(BoatType type)
    {
        return type switch
        {
            BoatType.Lancha => 5f,
            BoatType.BarcoComun => 3.5f,
            BoatType.Crucero => 2.2f,
            _ => 3f
        };
    }
}
