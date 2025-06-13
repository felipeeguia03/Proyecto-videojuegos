using UnityEngine;

public class BoatDissolver : MonoBehaviour
{
    private Material mat;
    private float dissolveAmount = 0f;
    private bool dissolving = false;

    public float speed = 1f;

    void Start()
    {
    
        mat = new Material(GetComponent<SpriteRenderer>().material);
        GetComponent<SpriteRenderer>().material = mat;
        mat.SetFloat("_DissolveAmount", dissolveAmount);
    }

    public void StartDissolve()
    {
        dissolving = true;
    }

    void Update()
    {
        if (dissolving)
        {
            dissolveAmount += Time.deltaTime * speed;
            mat.SetFloat("_DissolveAmount", dissolveAmount);

            if (dissolveAmount >= 1f)
                gameObject.SetActive(false); // o return to pool
        }
    }
}
