using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float distanciaAtaque = 2f;
    public float tempoEntreAtaques = 2f;

    private float proximoAtaque = 0f;

    private GameObject jogador;
    private NavMeshAgent agente;
    private Animator animator;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (jogador == null) return;

        float distancia = Vector3.Distance(transform.position, jogador.transform.position);

        if (distancia <= distanciaAtaque)
        {
            agente.isStopped = true;

            if (Time.time >= proximoAtaque)
            {
                animator.SetTrigger("Atacar");

                PlayerHealth vida = jogador.GetComponent<PlayerHealth>();
                if (vida != null)
                {
                    vida.TomarDano(1);
                }

                proximoAtaque = Time.time + tempoEntreAtaques;
            }
        }
        else
        {
            agente.isStopped = false;
            agente.SetDestination(jogador.transform.position);
        }
    }
}
