using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.2f;
    public Color flashColor = new Color(1, 0, 0, 0.5f);

    private bool estaMorto = false;

    public void Flash()
    {
        if (!estaMorto)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    IEnumerator FlashRoutine()
    {
        flashImage.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        flashImage.color = new Color(1, 0, 0, 0);
    }

    public void SetarMorto(bool morto)
    {
        estaMorto = morto;

        if (morto && flashImage != null)
        {
            flashImage.color = new Color(1, 0, 0, 0);
        }
    }
}

