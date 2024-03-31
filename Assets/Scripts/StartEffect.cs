using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartEffect : MonoBehaviour
{
    public Vector3 targetTrs;


    public Transform[] starList;


    public void SetTarget(Vector3 _target)
    {
        targetTrs = _target;
    }

    public void Fly()
    {
        for (int i = 0; i < starList.Length; i++)
        {
            starList[i].localScale = new Vector3(.5f, .5f, .5f);
        }

        MoveStar();
        StartCoroutine(PlayHitSound());
    }

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveStar()
    {
        float delay = 0.0f;
        for(int i = 0; i < starList.Length; i++)
        {                   
            starList[i].gameObject.SetActive(true);
            starList[i].DOScale(1f, 0.3f).SetDelay(0f).SetEase(Ease.Linear);
            starList[i].DOMove(new Vector3(targetTrs.x , targetTrs.y , 0f), 0.3f).SetDelay(delay).SetEase(Ease.Linear);
            delay += 0.1f;
        }

        for (int i = 0; i < starList.Length; i++)
        {
            StartCoroutine(HideStar(starList[i].gameObject, 0.3f + i * 0.1f));
            
        }

           
    }

    public IEnumerator HideStar(GameObject starObj, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        starObj.gameObject.SetActive(false);
    }

    IEnumerator PlayHitSound()
    {
        yield return new WaitForSeconds((starList.Length -1)* 0.1f + 0.3f);
        AudioManager.instance.hitStarBtn.Play();
    }
}
