using UnityEngine;

public class MorteInstantanea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth vida = GetComponent<PlayerHealth>();
            if (vida != null)
            {
                vida.TomarDano(vida.vidaMax);
            }
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
