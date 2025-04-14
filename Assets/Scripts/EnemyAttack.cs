using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float distanciaAtaque = 2f;
    public float tempoEntreAtaques = 2f;
    private float proximoAtaque = 0f;

    private GameObject jogador;
    private Animator animator;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (jogador == null) return;

        float distancia = Vector3.Distance(transform.position, jogador.transform.position);

        if (distancia <= distanciaAtaque && Time.time >= proximoAtaque)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        if (animator != null)
        {
            animator.SetTrigger("Atacar");
        }

        PlayerHealth vidaPlayer = jogador.GetComponent<PlayerHealth>();
        if (vidaPlayer != null)
        {
            vidaPlayer.TomarDano(1);
        }

        proximoAtaque = Time.time + tempoEntreAtaques;
    }
}

