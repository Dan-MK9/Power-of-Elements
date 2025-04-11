using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private CameraShake cameraShake;

    private ScreenFlash telaFlash;

    public int vidaMax = 3;
    private int vidaAtual;

    public Image[] coracoes;
    public Sprite coracaoCheio;
    public Sprite coracaoVazio;

    public bool podeMover = true;

    public GameObject gameOverPanel;

    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();

        telaFlash = FindObjectOfType<ScreenFlash>();

        vidaAtual = vidaMax;
        AtualizarHUD();
    }

    public void TomarDano(int dano)
    {
        Debug.Log("Tomou dano!");
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMax);

        AtualizarHUD();

        if (vidaAtual <= 0) {
        {
            Morreu();
        }
    }
        if (telaFlash != null)
            telaFlash.Flash();

        if (cameraShake != null)
            cameraShake.Tremer();
    }

    void AtualizarHUD ()
        {
            for (int i = 0; i < coracoes.Length; i++)
            {
                if (i < vidaAtual)

                    coracoes[i].sprite = coracaoCheio;
                else
                    coracoes[i].sprite = coracaoVazio;
            }
        }

    void Morreu ()
    {
        Cursor.lockState = CursorLockMode.None ;

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
            TomarDano(1);
        }
    }
}
