using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class UIManager : Singleton<UIManager>
{
    
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;
    public string curentScene;
    public string nextScene;
    
    // Start is called before the first frame update
    void Start()
    {
        
        OnInit();
    }

    public void OnInit()
    {

    }


    public void Replay()
    {

        SceneManager.LoadScene(curentScene);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Win()
    {
        GameManager.instance.isFinish = true;
        StartCoroutine(ActivateUI(winUI));
        
    }

    public void Lose()
    {
        GameManager.instance.isFinish = true;
        StartCoroutine(ActivateUI(loseUI));
        
    }

    IEnumerator ActivateUI(GameObject ui)
    {
        yield return new WaitForSeconds(3f);
        ui.SetActive(true);
    }
}
