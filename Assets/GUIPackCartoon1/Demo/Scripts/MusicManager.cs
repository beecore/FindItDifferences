// Copyright (C) 2015-2021 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.UI;

namespace Ricimi
{
   
    public class MusicManager : MonoBehaviour
    {
        private Slider m_musicSlider;
        private GameObject m_musicButton;

        private void Start()
        {
            m_musicSlider = GetComponent<Slider>();
            if (PlayerPrefs.GetInt("Music") == 0)
            {
                m_musicSlider.value = 1;
            }
            else
            {
                m_musicSlider.value = 0;
            }
        }

        public void SwitchMusic()
        {
            int isChoose = (int)m_musicSlider.value;
            

            if (isChoose == 1)
            {
                PlayerPrefs.SetInt("Music", 0);
                AudioManager.instance.ToogleMusic(true);
            }
            else
            {
                PlayerPrefs.SetInt("Music",1);
                AudioManager.instance.ToogleMusic(false);
            }

          
        }
    }
}
