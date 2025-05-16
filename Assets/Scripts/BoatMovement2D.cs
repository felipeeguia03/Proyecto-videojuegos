using UnityEngine;

public class BoatMovement2D : MonoBehaviour
{
    public float speed = 3f;
    private Vector3 target;
    private BoatType boatType;
    private BoatPool pool;
    private bool volviendo = false;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(BoatType type, BoatPool boatPool)
    {
        boatType = type;
        pool = boatPool;
        volviendo = false;
        gameObject.SetActive(true);
    }

    public void SetTarget(Vector3 t)
    {
        target = t;
        Vector3 dir = (target - transform.position).normalized;

        // Aplicar rotación hacia el objetivo
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void Update()
    {
        // Movimiento hacia el objetivo
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Si llegó al destino
        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            if (pool != null)
            {
                pool.ReturnBoat(boatType, gameObject);
            }
            else
            {
                Debug.LogWarning("Pool no asignado, destruyendo el barco");
                Destroy(gameObject);
            }
        }
    }

  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Luz") && !volviendo)
        {
            Debug.Log("funciona");

            volviendo = true;

            Vector3 dir = (transform.position - target).normalized;
            Vector3 newTarget = transform.position + dir * 20f;
            SetTarget(newTarget);
        }
    }
}
