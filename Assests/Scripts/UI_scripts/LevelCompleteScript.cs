using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour
{
    public Transform man;
    public Transform finishLine;
    public GameObject LevelCompletePanel;

    public static bool isLevelComplete = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(man.position.x > finishLine.position.x && isLevelComplete == false)
        {
            isLevelComplete = true;
            LevelCompletePanel.SetActive(true);
            StartCoroutine(delayLevelStart());
        }
    }

    IEnumerator delayLevelStart()
    {
        yield return new WaitForSeconds(5);
        isLevelComplete = false;
        SceneManager.LoadScene("Level2");
    }
}
