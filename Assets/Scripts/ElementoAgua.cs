using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoAgua : MonoBehaviour
{
    public int dano = 1;
    public float intervaloDano = 1.5f;
    public float slowDuration = 3f;
    public float slowMultiplier = 0.5f;

    private Dictionary<GameObject, float> proximoTempoDeDano = new Dictionary<GameObject, float>();
    private HashSet<GameObject> jogadoresNaAgua = new HashSet<GameObject>();
    private Dictionary<GameObject, Coroutine> corrotinasLentidao = new Dictionary<GameObject, Coroutine>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth vida = other.GetComponent<PlayerHealth>();
            Player movimento = other.GetComponent<Player>();

            if (vida != null)
            {
                vida.TomarDano(dano);
            }

            if (movimento != null && !jogadoresNaAgua.Contains(other.gameObject))
            {
                jogadoresNaAgua.Add(other.gameObject);
                movimento.velocidade *= slowMultiplier;

                if (corrotinasLentidao.ContainsKey(other.gameObject))
                {
                    StopCoroutine(corrotinasLentidao[other.gameObject]);
                    corrotinasLentidao.Remove(other.gameObject);
                }
            }

            if (!proximoTempoDeDano.ContainsKey(other.gameObject))
            {
                proximoTempoDeDano.Add(other.gameObject, Time.time + intervaloDano);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth vida = other.GetComponent<PlayerHealth>();
            if (vida != null && proximoTempoDeDano.ContainsKey(other.gameObject))
            {
                if (Time.time >= proximoTempoDeDano[other.gameObject])
                {
                    vida.TomarDano(dano);
                    proximoTempoDeDano[other.gameObject] = Time.time + intervaloDano;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (proximoTempoDeDano.ContainsKey(other.gameObject))
            {
                proximoTempoDeDano.Remove(other.gameObject);
            }

            if (jogadoresNaAgua.Contains(other.gameObject))
            {
                jogadoresNaAgua.Remove(other.gameObject);
                Player movimento = other.GetComponent<Player>();

                if (movimento != null)
                {
                    Coroutine c = StartCoroutine(RestaurarVelocidadeDepois(movimento, slowDuration));
                    corrotinasLentidao[other.gameObject] = c;
                }
            }
        }
    }

    private IEnumerator RestaurarVelocidadeDepois(Player movimento, float delay)
    {
        GameObject jogador = movimento.gameObject;
        float velocidadeOriginal = movimento.velocidade / slowMultiplier;

        yield return new WaitForSeconds(delay);

        if (!jogadoresNaAgua.Contains(jogador))
        {
            movimento.velocidade = velocidadeOriginal;
            corrotinasLentidao.Remove(jogador);
        }
    }
}


