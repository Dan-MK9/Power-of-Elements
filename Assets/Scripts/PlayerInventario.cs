using UnityEngine;
using TMPro;

public class PlayerInventario : MonoBehaviour
{
    public int cristais = 0;
    public TextMeshProUGUI textoCristais;

    public Player player;

    public void AdicionarCristal(int valor)
    {
        cristais ++;
        AtualizarHUD();

        if (cristais == 5 && player != null)
        {
            player.DesbloquearPuloDuplo();
        }
    }

    void AtualizarHUD()
    {
        if (textoCristais != null)
            textoCristais.text = "Cristais coletados: 0/" + cristais;
    }
}

