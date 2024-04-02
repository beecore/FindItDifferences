using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public GameObject SoundOn, SoundOff, MusicOn, MusicOff;

    // Start is called before the first frame update
    void Start()
    {
        LoadSettingInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClosePanel()
    {
        AudioManager.instance.clickBtn.Play();
        OnHide();
       
    }

    private void OnHide()
    {
        gameObject.SetActive(false);
    }

    void LoadSettingInfo()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            AudioManager.instance.ToogleSound(true);
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
        }
        else
        {
            AudioManager.instance.ToogleSound(false);
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Music") == 0)
        {
            AudioManager.instance.ToogleMusic(true);
            MusicOn.SetActive(true);
            MusicOff.SetActive(false);
        }
        else
        {
            AudioManager.instance.ToogleMusic(false);
            MusicOn.SetActive(false);
            MusicOff.SetActive(true);
        }

        
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
            AudioManager.instance.ToogleSound(false);
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            AudioManager.instance.ToogleSound(true);
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
        }

    }

    public void ToggleMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            PlayerPrefs.SetInt("Music", 1);
            AudioManager.instance.ToogleMusic(false);
            MusicOn.SetActive(false);
            MusicOff.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            AudioManager.instance.ToogleMusic(true);
            MusicOn.SetActive(true);
            MusicOff.SetActive(false);
        }

    }
}
