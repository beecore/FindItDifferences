// Copyright (C) 2015-2021 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.UI;

namespace Ricimi
{
    // This class handles updating the sound UI widgets depending on the player's selection.
    public class SoundManager : MonoBehaviour
    {
        private Slider m_soundSlider;
        private GameObject m_soundButton;

        private void Start()
        {
            m_soundSlider = GetComponent<Slider>();
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                m_soundSlider.value = 1;
            }
            else
            {
                m_soundSlider.value = 0;
            }
        }

        public void SwitchSound()
        {
            int isChoose = (int)m_soundSlider.value;


            if (isChoose == 1)
            {
                PlayerPrefs.SetInt("Sound", 0);
                AudioManager.instance.ToogleSound(true);
            }
            else
            {
                PlayerPrefs.SetInt("Sound", 1);
                AudioManager.instance.ToogleSound(false);
            }
        }
    }
}
