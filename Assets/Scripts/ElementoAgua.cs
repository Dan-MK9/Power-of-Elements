using System.Collections;
using UnityEngine;

public class ElementoAgua : MonoBehaviour
{
    public int dano = 1;
    public float danoDuration = 3f;
    public float slowDuration = 3f;
    public float slowMultiplier = 0.5f;
    private PlayerHealth vida;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (vida != null)
            {
                vida.TomarDano(dano);
                //StartCoroutine(Damage(vida));
            }

            Player movimento = other.GetComponent<Player>();
            if (movimento != null)
            {
                StartCoroutine(AplicarLentidao(movimento));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }


    private System.Collections.IEnumerator AplicarLentidao(Player movimento)
    {
        float velocidadeOriginal = movimento.velocidade;
        movimento.velocidade *= slowMultiplier;

        yield return new WaitForSeconds(slowDuration);

        movimento.velocidade = velocidadeOriginal;
    }

    //private System.Collections.IEnumerator Damage(PlayerHealth damage)
    //{
    //    vida.TomarDano(1);
    //    yield return new WaitForSeconds(danoDuration);

    //}

    void Start()
    {
        vida = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        
    }
}
