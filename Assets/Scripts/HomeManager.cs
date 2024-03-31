using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeManager : MonoBehaviour
{
    public GameObject settingPanel;

    public TextMeshProUGUI levelText;

    void Start()
    {
        levelText.text = "LEVEL " + (PlayerPrefs.GetInt("Level") + 1).ToString();
    }


    public void PlayGame()
    {
        AudioManager.instance.clickBtn.Play();
        SceneManager.LoadScene("Game");
    }
    public void PlayChalennger()
    {
        AudioManager.instance.clickBtn.Play();
        SceneManager.LoadScene("Chalennger");
    }
    
    public void ShowSetting()
    {
        AudioManager.instance.clickBtn.Play();
        settingPanel.SetActive(true);
    }

}
