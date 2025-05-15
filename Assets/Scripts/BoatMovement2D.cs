using UnityEngine;

public class BoatMovement2D : MonoBehaviour
{
    public float speed = 3f;
    private Vector3 target;
    private SoundManager soundmanager;
    public void SetTarget(Vector3 t)
    {
        target = t;
        Vector3 direction = (t - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void Update()
    {
        soundmanager = FindFirstObjectByType<SoundManager>();
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            Debug.Log("Barco se estrelló");
            soundmanager.SeleccionAudio();
            Destroy(gameObject);
        }
    }
}