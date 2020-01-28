using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject creditsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showCredits()
    {
       startPanel.SetActive(false);
       creditsPanel.SetActive(true);
    }

    public void backButton()
    {
        creditsPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void startButton()
    {
       SceneManager.LoadScene("Level1");
    }
}
