using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private CameraShake cameraShake;

    private ScreenFlash telaFlash;

    public float vidaMax = 3f;
    private float vidaAtual;

    public Image[] coracoes;
    public Sprite coracaoCheio;
    public Sprite coracaoMeio;
    public Sprite coracaoVazio;

    public bool podeMover = true;
    public bool estaInvencivel = false;

    public GameObject gameOverPanel;

    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        telaFlash = FindObjectOfType<ScreenFlash>();
        vidaAtual = vidaMax;
        AtualizarHUD();
    }

    public void TomarDano(float dano)
    {
        Debug.Log("Tomou dano!");
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0f, vidaMax);

        AtualizarHUD();

        if (vidaAtual <= 0f)
        {
            Morreu();
        }

        if (telaFlash != null)
            telaFlash.Flash();

        if (cameraShake != null)
            cameraShake.Tremer();

        if (estaInvencivel)
        {
            Debug.Log("Dano bloqueado");
            return;
        }
    }

    void AtualizarHUD()
    {
        for (int i = 0; i < coracoes.Length; i++)
        {
            float coracaoIndex = i;

            if (coracaoIndex + 1 <= vidaAtual)
            {
                coracoes[i].sprite = coracaoCheio;
            }
            else if (coracaoIndex + 0.5f <= vidaAtual)
            {
                coracoes[i].sprite = coracaoMeio;
            }
            else
            {
                coracoes[i].sprite = coracaoVazio;
            }
        }
    }

    void Morreu()
    {
        Cursor.lockState = CursorLockMode.None;

        FirstPersonalCamera cameraLook = FindAnyObjectByType<FirstPersonalCamera>();
        if (cameraLook != null)
        {
            cameraLook.podeOlhar = false;
        }

        gameOverPanel.SetActive(true);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<Player>().podeMover = false;
        }

        if (telaFlash != null)
        {
            telaFlash.SetarMorto(true);
        }

        Debug.Log("O jogador morreu!");
    }

    void Update()
    {
        if (!podeMover) return;

        if (Input.GetKeyDown(KeyCode.H))
        {
            TomarDano(0.5f);
        }
    }

    public IEnumerator InvencibilidadeTemporaria(float tempo)
    {
        estaInvencivel = true;
        Debug.Log("O jogador está invencivel");

        yield return new WaitForSeconds(tempo);

        estaInvencivel = false;
        Debug.Log("Invencibilidade acabou");
    }
}