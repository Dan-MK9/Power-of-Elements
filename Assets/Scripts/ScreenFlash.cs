using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.2f;
    public Color flashColor = new Color(1, 0, 0, 0.5f);

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        flashImage.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        flashImage.color = new Color(1, 0, 0, 0);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
