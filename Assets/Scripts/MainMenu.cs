using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
	public Animator transition;

    public void PlayGame()
    {
    	StartCoroutine(loadlevel());
    }

    IEnumerator loadlevel()
    {
    	transition.SetTrigger("Start");
    	yield return new WaitForSeconds(1);
    	SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
    	Application.Quit();
    }
}
