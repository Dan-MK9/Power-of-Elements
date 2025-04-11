using UnityEngine;
using TMPro;

public class PlayerInventario : MonoBehaviour
{
    public int cristais = 0;
    public TextMeshProUGUI textoCristais;

    public void AdicionarCristal(int valor)
    {
        cristais += valor;
        AtualizarHUD();
    }

    void AtualizarHUD()
    {
        if (textoCristais != null)
            textoCristais.text = "Cristais coletados: 5/" + cristais;
    }
}

