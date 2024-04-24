using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    Toggle m_Toggle;
    // Start is called before the first frame update
    void Start()
    {
        m_Toggle = GetComponent<Toggle>();
       
        int allowNotification=PlayerPrefs.GetInt("allowNotification", 0);
        if (allowNotification == 1)
        {
            m_Toggle.isOn = true;
        }else
        {
            m_Toggle.isOn = false;
        }
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
    }
    void ToggleValueChanged(Toggle change)
    {
        if (m_Toggle.isOn)
        {
            PlayerPrefs.SetInt("allowNotification", 1);
        }else
        {
            PlayerPrefs.SetInt("allowNotification", 0);

        }
    }

}
