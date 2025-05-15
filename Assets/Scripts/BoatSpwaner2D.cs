using UnityEngine;

public class BoatSpawner2D : MonoBehaviour
{
    public GameObject[] boatPrefabs;
    public Transform lighthouseCenter;
    public float spawnRadius = 10f;
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

        int randomIndex = Random.Range(0, boatPrefabs.Length);
        GameObject boat = Instantiate(boatPrefabs[randomIndex], spawnPos, Quaternion.identity);

        BoatMovement2D moveScript = boat.GetComponent<BoatMovement2D>();

        switch (boat.name.Replace("(Clone)", "").Trim())
        {
            case "Lancha":
                moveScript.speed = 5f;
                break;
            case "BarcoComun":
                moveScript.speed = 3.5f;
                break;
            case "Crucero":
                moveScript.speed = 2.5f;
                break;
        }

        moveScript.SetTarget(lighthouseCenter.position);
    }
}