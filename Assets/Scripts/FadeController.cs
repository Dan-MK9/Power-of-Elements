using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeOutAndRestart ()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    IEnumerable FadeIn()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime / fadeDuration;
            SetAlpha(t);
            yield return null;
        }
    }

    IEnumerable FadeOutAndLoad()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            SetAlpha( t );
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetAlpha(float a)
    {
        Color c = fadeImage.color;
        c.a = Mathf.Clamp01(a);
        fadeImage.color = c;
    }

    void Update()
    {
        
    }
}
