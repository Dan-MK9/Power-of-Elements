using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private CameraShake cameraShake;

    private ScreenFlash telaFlash;

    public int vidaMax = 3;
    private int vidaAtual;

    public Image[] coracoes;
    public Sprite coracaoCheio;
    public Sprite coracaoVazio;

    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();

        telaFlash = FindObjectOfType<ScreenFlash>();

        vidaAtual = vidaMax;
        AtualizarHUD();
    }

    public void TomarDano(int dano)
    {
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
            Debug.Log("O jogador morreu!");
            FindObjectOfType<FadeController>().FadeOutAndRestart();
        }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TomarDano(1);
        }
    }
}
