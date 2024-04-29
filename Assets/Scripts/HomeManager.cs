using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public GameObject settingPanel;

    private void PlayButtonClickSound()
    {
        AudioManager.instance.clickBtn.Play();
    }

    public void PlayGame(int type)
    {
        PlayButtonClickSound();
        GameConfig.typeGame = type;

        string sceneName = (type == 0) ? "Game" : "Challenge";
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleSettingPanel(bool show)
    {
        PlayButtonClickSound();
        settingPanel.SetActive(show);
    }

    public void ShowSetting()
    {
        ToggleSettingPanel(true);
    }

    public void CloseSetting()
    {
        ToggleSettingPanel(false);
    }

    public void OnClickRateUs()
    {
        PlayButtonClickSound();
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}