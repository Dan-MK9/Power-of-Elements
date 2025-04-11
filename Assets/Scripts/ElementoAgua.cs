using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoAgua : MonoBehaviour
{
    public int dano = 1;
    public float intervaloDano = 3f;
    public float slowDuration = 3f;
    public float slowMultiplier = 0.5f;

    private Dictionary<GameObject, float> proximoTempoDeDano = new Dictionary<GameObject, float>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth vida = other.GetComponent<PlayerHealth>();
            if (vida != null)
            {
                vida.TomarDano(dano);
            }

            Player movimento = other.GetComponent<Player>();
            if (movimento != null)
            {
                StartCoroutine(AplicarLentidao(movimento));
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
        if (proximoTempoDeDano.ContainsKey(other.gameObject))
        {
            proximoTempoDeDano.Remove(other.gameObject);
        }
    }

    private IEnumerator AplicarLentidao(Player movimento)
    {
        float velocidadeOriginal = movimento.velocidade;
        movimento.velocidade *= slowMultiplier;

        yield return new WaitForSeconds(slowDuration);

        movimento.velocidade = velocidadeOriginal;
    }
}

