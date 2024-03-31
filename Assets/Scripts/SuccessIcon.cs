using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SuccessIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(1.25f, 0.25f).SetDelay(0f).SetEase(Ease.OutBack).OnComplete(() => OnBack());
    }

    void OnBack()
    {
        transform.DOScale(1.0f, 0.25f).SetDelay(0f).SetEase(Ease.OutBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
