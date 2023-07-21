using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsUIScript : MonoBehaviour
{
    public GameObject panel;

    private Image panelImage;

    private void Awake()
    {
        panelImage = panel.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartWait");
    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();
    }

    IEnumerator StartWait()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine("FadeIn");
    }
    IEnumerator FadeIn()
    {
        float alphaValue = panelImage.color.a;
        Color tmp = panelImage.color;

        while (alphaValue > 0)
        {
            alphaValue -= .01f;
            tmp.a = alphaValue;
            panelImage.color = tmp;

            yield return new WaitForSeconds(.05f);
        }


    }
}
