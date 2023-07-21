using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject shop, lose;
    public bool pause;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            shop.SetActive(!shop.activeInHierarchy);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pause = true;
    }

    public void EndGame()
    {
        Pause();
        lose.SetActive(true);
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
