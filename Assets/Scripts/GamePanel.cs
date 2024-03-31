using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class GamePanel : MonoBehaviour
{
    public TextMeshProUGUI timerTxt;

    public TextMeshProUGUI minusTxt;

    private Guid uid;

    private Sequence mySequence;
    public GameObject settingPanel;
    public void ShowSetting()
    {
        AudioManager.instance.clickBtn.Play();
        settingPanel.SetActive(true);
    }

    public void ShowTimer(float timeValue)
    {
        //var ts = TimeSpan.FromSeconds(timeValue);
        timerTxt.text = TimeSpan.FromSeconds(timeValue).ToString(@"mm\:ss");
    }

    public void ShowMinusTimer()
    {
        if(mySequence != null)
        {
            DOTween.Kill(uid);
            mySequence = null;
        }

        minusTxt.color = new Color(0.95f, 0f, 0f, 1f);
        mySequence = DOTween.Sequence();
        mySequence.Append(minusTxt.DOColor(new Color(0.95f,0f,0f,0f), 1.5f).SetDelay(0f).SetEase(Ease.OutBack));
        uid = System.Guid.NewGuid();
        mySequence.id = uid;
    }
}
