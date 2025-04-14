using TMPro;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //======================== MOVIMENTO ========================
    public float velocidade = 6f;

    CharacterController controller;
    Vector3 forward, strafe, vertical;
    float gravity, jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    public bool podeMover = true;

    //======================== PULO DUPLO ========================
    private bool podePuloDuplo = false;
    private bool usouPuloDuplo = false;
    [SerializeField] private CanvasGroup mensagemPuloDuplo;
    private bool mensagemMostrada = false;
    [SerializeField] private GameObject jumpIcon;

    //======================== VELOCIDADE ========================
    private bool velocidadeDesbloqueada = false;
    [SerializeField] private float velocidadeUpgrade = 8f;
    [SerializeField] private CanvasGroup mensagemVelocidade;
    [SerializeField] private GameObject speedIcon;

    //============================================================

    void Start()
    {
        controller = GetComponent<CharacterController>();

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;

        if (jumpIcon != null) jumpIcon.SetActive(false);
        if (speedIcon != null) speedIcon.SetActive(false);
        if (mensagemPuloDuplo != null) mensagemPuloDuplo.gameObject.SetActive(false);
        if (mensagemVelocidade != null) mensagemVelocidade.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!podeMover) return;

        // Movimento
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        forward = forwardInput * velocidade * transform.forward;
        strafe = strafeInput * velocidade * transform.right;
        vertical += gravity * Time.deltaTime * Vector3.up;

        if (controller.isGrounded)
        {
            vertical = Vector3.down;
            usouPuloDuplo = false;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (controller.isGrounded)
            {
                vertical = jumpSpeed * Vector3.up;
            }
            else if (podePuloDuplo && !usouPuloDuplo)
            {
                vertical = jumpSpeed * Vector3.up;
                usouPuloDuplo = true;
            }
        }

        // Aplicar movimento
        Vector3 finalVelocity = forward + strafe + vertical;
        controller.Move(finalVelocity * Time.deltaTime);

        // Verificações de upgrades
        VerificarDesbloqueioPuloDuplo();
    }

    //=================== FUNÇÕES DE HABILIDADES =======================

    void VerificarDesbloqueioPuloDuplo()
    {
        PlayerInventario inventario = GetComponent<PlayerInventario>();
        if (inventario != null && inventario.cristais >= 5 && !podePuloDuplo)
        {
            podePuloDuplo = true;
            Debug.Log("Pulo duplo desbloqueado!");

            if (jumpIcon != null) jumpIcon.SetActive(true);
            if (mensagemPuloDuplo != null)
                StartCoroutine(FadeMensagemPuloDuplo());
        }
    }

    public void DesbloquearPuloDuplo()
    {
        podePuloDuplo = true;
        Debug.Log("Pulo duplo desbloqueado!");
    }

    public void DesbloquearVelocidade()
    {
        if (!velocidadeDesbloqueada)
        {
            velocidadeDesbloqueada = true;
            velocidade = velocidadeUpgrade;

            if (speedIcon != null) speedIcon.SetActive(true);
            Debug.Log("Velocidade aprimorada desbloqueada!");

            if (mensagemVelocidade != null)
                StartCoroutine(FadeMensagemVelocidade());
        }
    }

    //==================== COROUTINES DE MENSAGEM ======================

    IEnumerator FadeMensagemPuloDuplo()
    {
        mensagemPuloDuplo.alpha = 0f;
        mensagemPuloDuplo.gameObject.SetActive(true);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            mensagemPuloDuplo.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            mensagemPuloDuplo.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        mensagemPuloDuplo.gameObject.SetActive(false);
    }

    IEnumerator FadeMensagemVelocidade()
    {
        mensagemVelocidade.alpha = 0f;
        mensagemVelocidade.gameObject.SetActive(true);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            mensagemVelocidade.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            mensagemVelocidade.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        mensagemVelocidade.gameObject.SetActive(false);
    }
}

