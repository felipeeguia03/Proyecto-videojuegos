using UnityEngine;

public class BoatMovement2D : MonoBehaviour
{
    public float speed = 3f;
    private Vector3 target;
    private BoatType boatType;
    private BoatPool pool;

    public void Initialize(BoatType type, BoatPool boatPool)
    {
        boatType = type;
        pool = boatPool;
    }

    public void SetTarget(Vector3 t)
    {
        target = t;
        Vector3 dir = (target - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            if (pool != null)
            {
                pool.ReturnBoat(boatType, this.gameObject);
            }
            else
            {
                Debug.LogWarning("Pool no asignado, destruyendo el barco");
                Destroy(gameObject);
            }
        }
    }
}
