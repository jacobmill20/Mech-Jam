using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditScroll : MonoBehaviour
{
    private RectTransform credits;
    public Image whiteS;

    private void Awake()
    {
        credits = this.GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (credits.localPosition.y < 1334)
        {
            credits.localPosition += Vector3.up;
        }
        else
        {
            StartCoroutine(FadeToWhite());
        }
    }

    IEnumerator FadeToWhite()
    {
        float alphaValue = whiteS.color.a;
        Color tmp = whiteS.color;

        while (alphaValue < 1)
        {
            alphaValue += .08f;
            tmp.a = alphaValue;
            whiteS.color = tmp;
            yield return new WaitForSeconds(.05f);
        }
        if (alphaValue >= 1)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
