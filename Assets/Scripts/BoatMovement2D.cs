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
        enabled = true;
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
            var dissolver = GetComponent<BoatDissolver>();
            if (dissolver != null)
            {
                dissolver.StartDissolve(); // 💥 empieza desintegración
                rb.linearVelocity = Vector2.zero; // detiene movimiento
                this.enabled = false;       // desactiva este script mientras se disuelve
            }
            else if (pool != null)
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
            Debug.Log("🚨 Barco iluminado: cambia de dirección y vuelve");

            volviendo = true;

            Vector3 dir = (transform.position - target).normalized;
            Vector3 newTarget = transform.position + dir * 20f;
            SetTarget(newTarget);
        }
    }
}
