using UnityEngine;

public class ColetavelCristal : MonoBehaviour
{
    public AudioClip somColeta;
    public GameObject efeitoColeta;

    public int valor = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventario inventario = other.GetComponent<PlayerInventario>();
            if (inventario != null)
            {
                inventario.AdicionarCristal(valor);
            }

            if (efeitoColeta != null)
            {
                Instantiate(efeitoColeta, transform.position, Quaternion.identity);
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
