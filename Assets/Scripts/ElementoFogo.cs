using System.Collections;
using UnityEngine;

public class ElementoFogo : MonoBehaviour
{
    public float danoInicial = 1f;
    public float danoContinuo = 0.5f;
    public float duracaoDano = 1.5f;

    private bool IsPlayerInFire = false;
    private Coroutine burnCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerHealth>().TakeDamage(danoInicial);
            IsPlayerInFire = true;

            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && IsPlayerInFire)
        {
            burnCoroutine = StartCoroutine(BurnDamageOverTime(other.GetComponent<PlayerHealth>()));
            IsPlayerInFire = false;
        }
    }

    IEnumerator BurnDamageOverTime(PlayerHealth playerHealth)
    {
        float elapsed = 0f;
        while (elapsed < duracaoDano)
        {
            //PlayerHealth.TakeDamage(danoContinuo);
            elapsed += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
