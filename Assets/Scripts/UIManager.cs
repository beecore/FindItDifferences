using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GamePanel mGamePanel;

    public GameObject gameWinPanel;

    public GameObject gameOverPanel;

    public GameObject pauseGamePanel;

    public TextMeshProUGUI levelText;


    public void ShowGameWin()
    {
        StartCoroutine(ShowGameWinIE());
    }

    public IEnumerator ShowGameWinIE()
    {
        yield return new WaitForSeconds(3.0f);
        AudioManager.instance.gameWinSfx.Play();
        gameWinPanel.SetActive(true);
        levelText.text = "LEVEL " + (GameManager.instance.currentLevelIndex + 1).ToString();
        GameManager.instance.currentLevelIndex++;
        PlayerPrefs.SetInt("Level", GameManager.instance.currentLevelIndex);
        yield return new WaitForSeconds(0.5f);
        AdsControl.Instance.ShowInterstitalMediation();
    }

    public void ShowGameOver()
    {
        StartCoroutine(ShowGameOverIE());
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }

    public void PauseGame()
    {
        GameManager.instance.currentState = GameManager.GAME_STATE.GAME_PAUSE;
        Time.timeScale = 0.0f;
        pauseGamePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        GameManager.instance.currentState = GameManager.GAME_STATE.GAME_PLAYING;
        Time.timeScale = 1.0f;
        pauseGamePanel.SetActive(false);
    }

    public IEnumerator ShowGameOverIE()
    {
        yield return new WaitForSeconds(1.0f);
        AudioManager.instance.gameOverSfx.Play();
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AdsControl.Instance.ShowInterstitalMediation();
    }
}
