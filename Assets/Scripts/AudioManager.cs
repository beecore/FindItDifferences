using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource clickBtn;

    public AudioSource correctBtn;

    public AudioSource failBtn;

    public AudioSource hitStarBtn;

    public AudioSource gameOverSfx;

    public AudioSource gameWinSfx;

    public AudioSource backgroundMusic;

    public AudioSource[] soundList;

    public static AudioManager instance;

    private void Awake()
    {
        
        int musicState = PlayerPrefs.GetInt("Music");
        if (musicState == 0)
        {
          
            ToogleMusic(true);
           

        }
        else
        {
           
            ToogleMusic(false);
        }

        int soundState = PlayerPrefs.GetInt("Sound");
        if (soundState == 0)
        {
           
            ToogleSound(true);

        }

        else
        {
         
            ToogleSound(false);
            
        }
        

        if (FindObjectsOfType(typeof(AudioManager)).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

       
        DontDestroyOnLoad(gameObject);
    }

   

    public void ToogleMusic(bool toogle)
    {
        if(toogle)
          backgroundMusic.volume = 0.5f;
        else
            backgroundMusic.volume = 0.0f;
    }

    public void ToogleSound(bool toogle)
    {
        if (toogle)
        {

            for (int i = 0; i < soundList.Length; i++)
                soundList[i].volume = 1.0f;

        }

        else
        {
            for (int i = 0; i < soundList.Length; i++)
                soundList[i].volume = 0.0f;


        }
    }
}
