using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator crossFade;
    public int MenuIndex;
    public int GameIndex;

    public float transTime = 1f;
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(ChangeScene(MenuIndex));
    }
    public void LoadGameScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(ChangeScene(GameIndex));
    }

    IEnumerator ChangeScene(int index)
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(transTime);
        SceneManager.LoadScene(index);
    }
    
    public void CloseGame()
    {
        StartCoroutine(ExitGame());
    }

    IEnumerator ExitGame()
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(transTime);
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
