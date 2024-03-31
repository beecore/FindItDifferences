using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MissIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0,0,15.0f), 0.1f).SetDelay(0f).SetEase(Ease.OutBack).OnComplete(() => OnBack());
    }

    void OnBack()
    {
        transform.DORotate(new Vector3(0,0,-15f), 0.1f).SetDelay(0f).SetEase(Ease.OutBack).OnComplete(() => OnRevert());
    }

    void OnRevert()
    {
        transform.DORotate(new Vector3(0, 0, 0f), 0.1f).SetDelay(0f).SetEase(Ease.OutBack).OnComplete(() => OnDestroy());
    }

    private void OnDestroy()
    {
        Destroy(gameObject, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
