using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duracao = 0.1f;
    public float intensidade = 0.3f;

    private Vector3 posicaoOriginal;

    void Start()
    {
        posicaoOriginal = transform.localPosition;
    }

    public void Tremer()
    {
        StartCoroutine(TremerCamera());
    }

    IEnumerator TremerCamera()
    {
        float tempo = 0f;

        while (tempo < duracao)
        {
            transform.localPosition = posicaoOriginal + Random.insideUnitSphere * intensidade;

            tempo += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = posicaoOriginal;
    }

    void Update()
    {
        
    }
}
