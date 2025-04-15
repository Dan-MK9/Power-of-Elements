using UnityEngine;

public class ColetavelInvencibilidade : MonoBehaviour
{
    public float duracao = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth vida = other.GetComponent<PlayerHealth>();
            if (vida != null)
            {
                vida.StartCoroutine(vida.InvencibilidadeTemporaria(duracao));
            }

            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
